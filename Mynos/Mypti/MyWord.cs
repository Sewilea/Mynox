using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWord : MonoBehaviour
{
    public float SizeX = 5, SizeZ = 5;
    public List<GameObject> Chunks;
    public List<Mypti> Timy;
    public GameObject Chunk_P;
    int seed;
    void Start()
    {
        Create();
    }
    
    void Update()
    {
        
    }

    private void Create()
    {
        seed = Random.Range(-999999, 999999);
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeX; z++)
            {
                GameObject obje = Instantiate(Chunk_P, new Vector3(x * 8, 0, z * 8), Quaternion.identity, gameObject.transform);
                obje.GetComponent<MyptiChunk>().seed = seed;
                obje.GetComponent<MyptiChunk>().chunk = gameObject;
                Vector3 Vk = gameObject.transform.position;
                Timy.Add(new Mypti(gameObject,false, 0, x * 8,0, z * 8));
                Chunks.Add(obje);
            }
        }
    }

    [System.Serializable]
    public class Mypti
    {
        public float x, y, z;
        public bool Air;
        public int BlockID;
        public GameObject Object;

        public Mypti(GameObject object2, bool air, int ID, float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
            Air = air;
            BlockID = ID;
            Object = object2;
        }
    }
}
