
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public SaveObject Ob;
    public float SizeX = 5, SizeZ = 5;
    public GameObject Chunk_P, Player;
    public List<chunks> ChunkData;
    public List<GameObject> Chunks;
    public int seed, a = 0, b = 0;
    public Menu men;

    [Header("try")]
    public bool trry;


    void Start()
    {
        if(Ob.Number == -1)
        {
            Ob.Value = 40;
        }
        SizeX = Ob.Value;
        SizeZ = Ob.Value;

        if (PlayerPrefs.GetInt("FSAS" + Ob.Number) == 1)
        {
            seed = PlayerPrefs.GetInt("FSEED" + Ob.Number);
        }
        else
        {
            seed = Random.Range(-9999999, 9999999);
        }

        if (PlayerPrefs.GetInt("FSAS" + Ob.Number) == 1 && Ob.Number == -1)
        {
            Ob.Flats = PlayerPrefs.GetInt("FSAF" + Ob.Number);
        }

        if(Ob.Map != 0)
        {
            Ob.Flats = 0;
            Ob.Mode = 1;
        }

        if(Ob.Map == 4 && Ob.Flats == 0)
        {
            Ob.Value = Ob.Maps.value;
            Maps_Create();
            Ob.Mode = Ob.Maps.Mode;
        }
        else if (Ob.Flats == 0)
        {
            Create();
            if (PlayerPrefs.GetInt("FSAS" + Ob.Number) == 1)
            {
                //men.Load_DestroyBlocks();
            }
        }
        else if (Ob.Flats == 1)
        {
            PerlinNoiseCreate();
        }
        
    }

    private void Create()
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                GameObject obje = Instantiate(Chunk_P, new Vector3(x * 2, 0, z * 2), Quaternion.identity, gameObject.transform);
                ChunkData.Add(new chunks(x, z,obje));
                obje.GetComponent<Chunk>().Chu = new chunks(x, z, obje);
                obje.GetComponent<Chunk>().seed = seed;
                obje.GetComponent<Chunk>().ChunkNumber = a;
                a++;
                Chunks.Add(obje);
            }
        }

        if(Ob.Map == 0)
        {
            if(PlayerPrefs.GetInt("FSAS" + Ob.Number) == 0)
            {
                Instantiate(Player, new Vector3(Random.Range(SizeX / 2, SizeX * 2), 14, Random.Range(SizeX / 2, SizeX * 2)), Quaternion.identity, null);
            }
            else
            {
                
                Instantiate(Player, new Vector3(PlayerPrefs.GetFloat("FS" + "X" + Ob.Number), PlayerPrefs.GetFloat("FS" + "Y" + Ob.Number), PlayerPrefs.GetFloat("FS" + "Z" + Ob.Number)), Quaternion.identity, null);
            }
        }
        else
        {
            if(Ob.Map == 3)
            {
                Instantiate(Player, new Vector3(Random.Range(SizeX / 2, SizeX * 2), 25, Random.Range(SizeX / 2, SizeX * 2)), Quaternion.identity, null);
            }
            else
            {
                Instantiate(Player, new Vector3(Random.Range(SizeX / 2, SizeX * 2), 7, Random.Range(SizeX / 2, SizeX * 2)), Quaternion.identity, null);
            }
        }
    }

    private void PerlinNoiseCreate()
    {
        
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                GameObject obje = Instantiate(Chunk_P, new Vector3(x * 2, 0, z * 2), Quaternion.identity, gameObject.transform);
                ChunkData.Add(new chunks(x, z, obje));
                obje.GetComponent<Chunk>().Chu = new chunks(x, z, obje);
                obje.GetComponent<Chunk>().seed = seed;
                obje.GetComponent<Chunk>().ChunkNumber = a;
                a++;
                Chunks.Add(obje);
            }
        }

        if (PlayerPrefs.GetInt("FSAS" + Ob.Number) == 0 || trry)
        {
            Instantiate(Player, new Vector3(Random.Range(SizeX / 2, SizeX * 2), 20, Random.Range(SizeZ / 2, SizeZ * 2)), Quaternion.identity, null);
        }
        else
        {
            Instantiate(Player, new Vector3(PlayerPrefs.GetFloat("FS" + "X" + Ob.Number), PlayerPrefs.GetFloat("FS" + "Y" + Ob.Number), PlayerPrefs.GetFloat("FS" + "Z" + Ob.Number)), Quaternion.identity, null);
        }

    }

    private void Maps_Create()
    {
        for (int x = 0; x < Ob.Maps.value; x++)
        {
            for (int z = 0; z < Ob.Maps.value; z++)
            {
                GameObject obje = Instantiate(Chunk_P, new Vector3(x * 2, 0, z * 2), Quaternion.identity, gameObject.transform);
                ChunkData.Add(new chunks(x, z, obje));
                obje.GetComponent<Chunk>().Chu = new chunks(x, z, obje);
                obje.GetComponent<Chunk>().seed = seed;
                obje.GetComponent<Chunk>().ChunkNumber = a;
                a++;
                Chunks.Add(obje);
            }
        }

        Instantiate(Player, new Vector3(Ob.Maps.CharX, Ob.Maps.CharY, Ob.Maps.CharZ), Quaternion.identity, null);
    }

    void Update()
    {

    }

    void DB()
    {
        men.Load_DestroyBlocks();
    }

    
}

[System.Serializable]

public class chunks
{
    public GameObject Object;
    public float x;
    public float z;

    public chunks(float x, float z, GameObject Object)
    {
        this.x = x;
        this.z = z;
        this.Object = Object;
    }

}
