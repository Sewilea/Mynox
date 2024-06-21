using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public World Cr;
    public SaveObject Ob;
    public chunks Chu;
    public List<chunks> Behind;
    public BlocksData data;
    public List<Vector3> Lanv, Lanb, Lanw, LanW, Lanwg, LanwG, Lang, LanGG;
    public List<GameObject> Block, Grass, Water, Glass;
    public List<Land> Lands;
    public List<DestroyBlock> DestB;
    public List<AddBlock> AddB;
    public float SizeX = 2, SizeY = 32, SizeZ = 2, SizeYN = 12, SizeReel = 0;
    public int ChunkNumber;
    public GameObject Player, Content;
    public bool aoi;
    public int seed;
    public float terDetail, td;

    public float amp = 10f;
    public float freq = 10f;

    public float Ores;

    bool time;
    float rTime;
    List<Vector3> Vks = new List<Vector3>();


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").gameObject;
        Content = gameObject.transform.GetChild(0).gameObject;
        Cr = GameObject.Find("World").gameObject.GetComponent<World>();
        SizeReel = Ob.SizeY;
        if (PlayerPrefs.GetInt("FSAS" + Ob.Number) == 1 && Ob.Number == -1)
        {
            Ob.Flats = PlayerPrefs.GetInt("FSAF" + Ob.Number);
            
        }

    }

    private void LateUpdate()
    {
        Vector3 Vk = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
        float Distance = Vector3.Distance(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), Vk);

        if (Distance < Ob.SeeAngular * (Ob.ChunkSize * 6))
        {
            if (aoi)
            {
                Content.SetActive(true);
            }
            else
            {
                if (Ob.Flats == 0)
                {
                    if (Ob.Number < 0)
                    {
                        if (Ob.Map == 1)
                            BikofyaCreate();
                        if (Ob.Map == 2)
                            WetSoil();
                        if (Ob.Map == 3)
                            SnowLand();
                        if (Ob.Map == 4)
                        {
                            MapCreate();
                        }
                    }
                    else
                    {
                        Create();
                    }
                }
                if (Ob.Flats == 1)
                {
                    PerlinNoiseCreate();
                }
            }
        }
        else
        {
            Content.SetActive(false);
        }
    }

    void Update()
    {
        if (time)
        {
            rTime = rTime + 1 * Time.deltaTime;
        }

        if(rTime > 0.5f)
        {
            rTime = 0;
            time = false;
            if (Vks.Count != 0)
            {
                for (int i = 0; i < Vks.Count; i++)
                {
                    Water_SC(Vks[i]);
                }
            }
        }

        
    }

    public void DestB_Add(DestroyBlock Block)
    {
        for (int i = 0; i < AddB.Count; i++)
        {
            if(AddB[i].x == Block.x && AddB[i].y == Block.y && AddB[i].z == Block.z)
            {
                AddB.RemoveAt(i);
            }
        }
        for (int i = 0; i < DestB.Count; i++)
        {
            if (DestB[i].x == Block.x && DestB[i].y == Block.y && DestB[i].z == Block.z)
            {
                DestB.RemoveAt(i);
            }
        }
        DestB.Add(new DestroyBlock(Block.x, Block.y, Block.z));
    }

    public void AddB_Add(AddBlock Block)
    {
        for (int i = 0; i < AddB.Count; i++)
        {
            if (AddB[i].x == Block.x && AddB[i].y == Block.y && AddB[i].z == Block.z)
            {
                AddB.RemoveAt(i);
            }
        }
        AddB.Add(new AddBlock(Block.x, Block.y, Block.z,Block.data));
    }

    private void BikofyaCreate()
    {
        aoi = true;
        /*
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                for (int y = 5; y < SizeY; y++)
                {
                    Vector3 Vk = gameObject.transform.position;
                    Lanv.Add(new Vector3(x + Vk.x, y, z + Vk.z));
                    Lands.Add(new Land(true, 0, x, y, z));
                }
            }
        }
        */
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                Vector3 Vk = gameObject.transform.position;
                int maxY = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / 32, (z + Vk.z + seed) / 32) * SizeYN);
                maxY += (int)4;

                for (int y = 0; y < SizeY; y++)
                {
                    if (y >= maxY)
                    {
                        Lanv.Add(new Vector3(x + Vk.x, y, z + Vk.z));

                        Lands.Add(new Land(true, 0, x, y, z));
                    }
                }
            }
        }
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                Vector3 Vk = gameObject.transform.position;
                int maxY = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / 32, (z + Vk.z + seed) / 32) * SizeYN);
                maxY += (int)4;

                GameObject Cover = data.Blocks[12], CoverUnder = data.Blocks[1];

                float By = 0;

                By = Mathf.PerlinNoise((seed + Vk.x + x) / (freq * 4f),
                    (Vk.z + z) / (freq * 4f)) * amp * 4f;
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / freq) * amp;
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / (freq * 0.5f)) * (amp * 0.5f);
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / (freq * 0.3f)) * (amp * 0.3f);

                Cover = data.Blocks[12];
                CoverUnder = data.Blocks[13];
                if (By > 42)
                {
                    Cover = data.Blocks[13];
                    CoverUnder = data.Blocks[13];
                }
                if (By < 18)
                {
                    Cover = data.Blocks[13];
                    CoverUnder = data.Blocks[13];
                }

                for (int y = 0; y < maxY; y++)
                {
                    if (y == maxY - 1)
                    {
                        GameObject Obje = Instantiate(Cover, new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));

                        /*
                        int TerDetail = 16;
                        int maxYA = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / terDetail, (z + Vk.z + seed) / terDetail) * SizeYN);
                        int maxYB = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / TerDetail, (z + Vk.z + seed) / TerDetail) * SizeYN);
                        int a = maxYA + maxYB;

                        if (maxYA + 1 == maxYB - 1)
                        {
                            Log_Structure(new Vector3(x + Vk.x, y + 1, z + Vk.z));
                        }
                        */
                        int a = Random.Range(0, 40);

                        if (a == 0)
                        {
                            if (Cover == data.Blocks[12])
                            {
                                Chest_Structure(new Vector3(x + Vk.x, y, z + Vk.z));
                            }
                            if (Cover == data.Blocks[13])
                            {
                                Kofia_Structure(new Vector3(x + Vk.x, y, z + Vk.z));
                            }
                        }

                    }
                    else if (y == 0)
                    {
                        GameObject Obje = Instantiate(data.Blocks[14], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));

                    }
                    else if (y < maxY - 4)
                    {
                        int Db = Random.Range(0, 70);

                        GameObject Obje = Instantiate(data.Blocks[13], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));

                    }
                    else
                    {
                        GameObject Obje = Instantiate(CoverUnder, new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                    }
                }
            }
        }


        FindBehind();
        Optimize_start();
        Optimize_start2();
    }

    private void SnowLand()
    {
        aoi = true;
        /*
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                for (int y = 5; y < SizeY; y++)
                {
                    Vector3 Vk = gameObject.transform.position;
                    Lanv.Add(new Vector3(x + Vk.x, y, z + Vk.z));
                    Lands.Add(new Land(true, 0, x, y, z));
                }
            }
        }
        */
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                Vector3 Vk = gameObject.transform.position;
                int maxY = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / 24, (z + Vk.z + seed) / 24) * SizeYN);
                maxY += (int)SizeYN;

                float By = 0;

                By = Mathf.PerlinNoise((seed + Vk.x + x) / (freq * 4f),
                    (Vk.z + z) / (freq * 4f)) * amp * 4f;
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / freq) * amp;
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / (freq * 0.5f)) * (amp * 0.5f);
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / (freq * 0.3f)) * (amp * 0.3f);

                if (By > 22)
                {
                    float Cliffs = Mathf.PerlinNoise((seed + Vk.x + x) / (freq * 4f),
                    (Vk.z + z) / (freq * 4f)) * amp * 4f;

                    if(Cliffs > 32)
                    {
                        maxY += 9;
                    }
                    else if (Cliffs > 31.5f)
                    {
                        maxY += 8;
                    }
                    else if (Cliffs > 31)
                    {
                        maxY += 7;
                    }
                    else if (Cliffs > 30.5f)
                    {
                        maxY += 6;
                    }
                    else if (Cliffs > 30)
                    {
                        maxY += 5;
                    }
                    else if (Cliffs > 29.5f)
                    {
                        maxY += 4;
                    }
                    else if (Cliffs > 29)
                    {
                        maxY += 3;
                    }
                    else if (Cliffs > 28.5f)
                    {
                        maxY += 2;
                    }
                    else if (Cliffs > 28)
                    {
                        maxY += 1;
                    }

                }

                for (int y = 0; y < SizeY; y++)
                {
                    if (y >= maxY)
                    {
                        Lanv.Add(new Vector3(x + Vk.x, y, z + Vk.z));

                        Lands.Add(new Land(true, 0, x, y, z));
                    }
                }
            }
        }
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                Vector3 Vk = gameObject.transform.position;
                int maxY = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / 24, (z + Vk.z + seed) / 24) * SizeYN);
                maxY += (int)SizeYN;

                GameObject Cover = data.Blocks[0], CoverUnder = data.Blocks[1];

                float By = 0;

                By = Mathf.PerlinNoise((seed + Vk.x + x) / (freq * 4f),
                    (Vk.z + z) / (freq * 4f)) * amp * 4f;
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / freq) * amp;
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / (freq * 0.5f)) * (amp * 0.5f);
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / (freq * 0.3f)) * (amp * 0.3f);

                Cover = data.Blocks[15];
                CoverUnder = data.Blocks[1];

                if (By > 22)
                {
                    float Cliffs = Mathf.PerlinNoise((seed + Vk.x + x) / (freq * 4f),
                    (Vk.z + z) / (freq * 4f)) * amp * 4f;

                    Cover = data.Blocks[15];
                    CoverUnder = data.Blocks[1];

                    if (Cliffs > 32)
                    {
                        maxY += 9;
                    }
                    else if (Cliffs > 31.5f)
                    {
                        maxY += 8;
                    }
                    else if (Cliffs > 31)
                    {
                        maxY += 7;
                    }
                    else if (Cliffs > 30.5f)
                    {
                        maxY += 6;
                    }
                    else if (Cliffs > 30)
                    {
                        maxY += 5;
                    }
                    else if (Cliffs > 29.5f)
                    {
                        maxY += 4;
                    }
                    else if (Cliffs > 29)
                    {
                        maxY += 3;
                    }
                    else if (Cliffs > 28.5f)
                    {
                        maxY += 2;
                    }
                    else if (Cliffs > 28)
                    {
                        maxY += 1;
                    }

                }
                if (By < 16)
                {
                    Cover = data.Blocks[17];
                    CoverUnder = data.Blocks[17];
                }

                for (int y = 0; y < maxY; y++)
                {
                    if (y == maxY - 1)
                    {
                        GameObject Obje = Instantiate(Cover, new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));

                        /*
                        int TerDetail = 16;
                        int maxYA = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / terDetail, (z + Vk.z + seed) / terDetail) * SizeYN);
                        int maxYB = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / TerDetail, (z + Vk.z + seed) / TerDetail) * SizeYN);
                        int a = maxYA + maxYB;

                        if (maxYA + 1 == maxYB - 1)
                        {
                            Log_Structure(new Vector3(x + Vk.x, y + 1, z + Vk.z));
                        }
                        */
                        int a = Random.Range(0, 60);

                        if (a == 0)
                        {
                            if (Cover == data.Blocks[15])
                            {
                                SnowLog_Structure(new Vector3(x + Vk.x, y, z + Vk.z));
                            }
                        }

                    }
                    else if (y == 0)
                    {
                        GameObject Obje = Instantiate(data.Blocks[7], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));

                    }
                    else if (y < maxY - 4)
                    {
                        int Db = Random.Range(0, 70);

                        if (Db > 65)
                        {
                            GameObject Obje = Instantiate(data.Blocks[6], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                            Block.Add(Obje);
                            Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                        }
                        else if (Db > 50)
                        {
                            GameObject Obje = Instantiate(data.Blocks[5], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                            Block.Add(Obje);
                            Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                        }
                        else
                        {
                            GameObject Obje = Instantiate(data.Blocks[2], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                            Block.Add(Obje);
                            Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                        }

                    }
                    else
                    {
                        GameObject Obje = Instantiate(CoverUnder, new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                    }
                }
            }
        }


        FindBehind();
        Optimize_start();
        Optimize_start2();
    }

    private void PerlinNoiseCreate()
    {
        aoi = true;
        /*
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                for (int y = 5; y < SizeY; y++)
                {
                    Vector3 Vk = gameObject.transform.position;
                    Lanv.Add(new Vector3(x + Vk.x, y, z + Vk.z));
                    Lands.Add(new Land(true, 0, x, y, z));
                }
            }
        }
        */
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                Vector3 Vk = gameObject.transform.position;
                int maxY = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / terDetail, (z + Vk.z + seed) / terDetail) * SizeYN);
                int g = maxY;
                maxY += (int)SizeYN; 
                float By = 0;

                By = Mathf.PerlinNoise((seed + Vk.x + x) / (freq * 4f),
                    (Vk.z + z) / (freq * 4f)) * amp * 4f;
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / freq) * amp;
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / (freq * 0.5f)) * (amp * 0.5f);
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / (freq * 0.3f)) * (amp * 0.3f);


                for (int y = 0; y < SizeY; y++)
                {
                    if(y >= maxY)
                    {
                        Lanv.Add(new Vector3(x + Vk.x, y, z + Vk.z));
                        
                        Lands.Add(new Land(true, 0, x, y, z));
                    }
                }
            }
        }
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                Vector3 Vk = gameObject.transform.position;
                int maxY = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / terDetail, (z + Vk.z + seed) / terDetail) * SizeYN);
                //Debug.Log(maxY);
                int g = maxY;
                maxY += (int)SizeYN;

                GameObject Cover = data.Blocks[0], CoverUnder = data.Blocks[1];

                float By = 0;

                By = Mathf.PerlinNoise((seed + Vk.x + x) / (freq * 4f),
                    (Vk.z + z) / (freq * 4f)) * amp * 4f;
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / freq) * amp;
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / (freq * 0.5f)) * (amp * 0.5f);
                By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                    (Vk.z + z) / (freq * 0.3f)) * (amp * 0.3f);


                int gh = 0;

                if (By > 44.8)
                {
                    Cover = data.Blocks[2];
                    CoverUnder = data.Blocks[2];
                    maxY += 1;
                }
                else if (By > 43.6)
                {
                    Cover = data.Blocks[2];
                    CoverUnder = data.Blocks[2];
                    maxY += 1;
                }
                else if(By > 42.8)
                {
                    Cover = data.Blocks[0];
                    CoverUnder = data.Blocks[1];
                    maxY += 0;
                }
                else if (By > 42)
                {
                    Cover = data.Blocks[0];
                    CoverUnder = data.Blocks[1];
                }
                else if (By > 16 && By < 28)
                {
                    if(g == 0)
                    {
                        Cover = data.Water[1];
                    }
                    else
                    {
                        Cover = data.Blocks[0];
                    }
                    CoverUnder = data.Blocks[1];
                }
                if (By < 18)
                {
                    Cover = data.Blocks[8];
                    CoverUnder = data.Blocks[8];
                }

                if(By > 20 && By < 26)
                {
                    gh = 1;
                }
                else if(By > 36 && By < 42)
                {
                    gh = 2;
                }

                for (int y = 0; y < maxY; y++)
                {
                    if (y == maxY - 1)
                    {
                        GameObject Obje = Instantiate(Cover, new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        if(Cover.tag == "Block")
                        {
                            Block.Add(Obje);
                            Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                        }
                        else
                        {
                            Lanv.Add(new Vector3(x + Vk.x, y, z + Vk.z));
                            Lanw.Add(new Vector3(x + Vk.x, y, z + Vk.z));
                            Water.Add(Obje);
                        }

                        /*
                        int TerDetail = 16;
                        int maxYA = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / terDetail, (z + Vk.z + seed) / terDetail) * SizeYN);
                        int maxYB = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / TerDetail, (z + Vk.z + seed) / TerDetail) * SizeYN);
                        int a = maxYA + maxYB;

                        if (maxYA + 1 == maxYB - 1)
                        {
                            Log_Structure(new Vector3(x + Vk.x, y + 1, z + Vk.z));
                        }
                        */

                        int Seedf = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / td, (z + Vk.z + seed) / td) * 36);
                        //Debug.Log(Seedf);

                        if(Seedf == 5)
                        {
                            if (Cover == data.Blocks[0])
                            {
                                Block_Add(data.Blocks[71], new Vector3(x + Vk.x, y + 1, z + Vk.z));
                            }
                        }
                        if (Seedf == 20 || Seedf == 4)
                        {
                            if(gh == 0)
                            {
                                if(Cover == data.Blocks[2])
                                {
                                    GameObject Obje3 = Instantiate(data.Grass[3], new Vector3(x + Vk.x, y + 1, z + Vk.z), Quaternion.identity, Content.transform);
                                    Grass.Add(Obje3);
                                    Lands.Add(new Land(false, 1, x + Vk.x, y + 1, z + Vk.z));
                                }
                                if (Cover == data.Blocks[0])
                                {
                                    if (Seedf == 20)
                                    {
                                        Birch_Log_Structure(new Vector3(x + Vk.x, y, z + Vk.z));
                                    }
                                    if (Seedf == 4)
                                    {
                                        Log_Structure(new Vector3(x + Vk.x, y, z + Vk.z));
                                    }
                                }
                                if (Cover == data.Blocks[8] && Seedf == 4)
                                {
                                    Cactus_Structure(new Vector3(x + Vk.x, y, z + Vk.z));
                                }
                            }
                            if(gh == 2)
                            {
                                if (Cover == data.Blocks[0])
                                {
                                    if (Seedf == 20)
                                    {
                                        Pine_Log_Structure(new Vector3(x + Vk.x, y, z + Vk.z));
                                    }
                                }
                            }
                        }
                        if(Seedf % 8 == 0)
                        {
                            if (Cover == data.Blocks[0])
                            {
                                GameObject Obje3 = Instantiate(data.Grass[0], new Vector3(x + Vk.x, y + 1, z + Vk.z), Quaternion.identity, Content.transform);
                                Grass.Add(Obje3);
                                Lands.Add(new Land(false, 1, x + Vk.x, y + 1, z + Vk.z));
                            }
                        }
                    }
                    else if (y == 0)
                    {
                        GameObject Obje = Instantiate(data.Blocks[7], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));

                    }
                    else if (y < maxY - 4)
                    {
                        int Seedf = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / td, (y + Vk.z + seed) / td) * Ores);
                        //Debug.Log(Seedf);


                        if(Seedf == 19)
                        {
                            GameObject Obje = Instantiate(data.Blocks[6], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                            Block.Add(Obje);
                            Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                        }
                        else if (Seedf == 22)
                        {
                            GameObject Obje = Instantiate(data.Blocks[69], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                            Block.Add(Obje);
                            Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                        }
                        else if(Seedf == 24)
                        {
                            GameObject Obje = Instantiate(data.Blocks[5], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                            Block.Add(Obje);
                            Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                        }
                        else if (Seedf == 8)
                        {
                            GameObject Obje = Instantiate(data.Blocks[25], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                            Block.Add(Obje);
                            Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                        }
                        else if (Seedf == 6)
                        {
                            GameObject Obje = Instantiate(data.Blocks[26], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                            Block.Add(Obje);
                            Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                        }
                        else
                        {
                            GameObject Obje = Instantiate(data.Blocks[2], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                            Block.Add(Obje);
                            Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                        }

                    }
                    else
                    {
                        GameObject Obje = Instantiate(CoverUnder, new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                    }
                }
            }
        }

        Cr.men.Load_Chunk(transform.position, gameObject, ChunkNumber);
        FindBehind();
    }

    private void MapCreate()
    {
        aoi = true;
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    Vector3 Vk = gameObject.transform.position;
                    Lanv.Add(new Vector3(x + Vk.x, y, z + Vk.z));
                    Lands.Add(new Land(true, 0, x, y, z));
                }
            }
        }
        Cr.men.MapLoadChunk(gameObject, transform.position, Ob.Maps.Chunks[ChunkNumber].MapBlock);
        FindBehind();
    }

    public void WetSoil()
    {
        aoi = true;
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                for (int y = 5; y < SizeY; y++)
                {
                    Vector3 Vk = gameObject.transform.position;
                    Lanv.Add(new Vector3(x + Vk.x, y, z + Vk.z));
                    Lands.Add(new Land(true, 0, x, y, z));
                }
            }
        }
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                for (int y = 0; y < 5; y++)
                {
                    Vector3 Vk = gameObject.transform.position;
                    float By = 0;

                    By = Mathf.PerlinNoise((seed + Vk.x + x) / (freq * 4f),
                        (Vk.z + z) / (freq * 4f)) * amp * 4f;
                    By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                        (Vk.z + z) / freq) * amp;
                    By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                        (Vk.z + z) / (freq * 0.5f)) * (amp * 0.5f);
                    By += Mathf.PerlinNoise((seed + Vk.x + x) / freq,
                        (Vk.z + z) / (freq * 0.3f)) * (amp * 0.3f);

                    GameObject Cover = data.Blocks[0];

                    if (By > 20)
                    {
                        Cover = data.Water[1];
                    }
                    if (y == 4)
                    {
                        if (By > 20)
                        {
                            GameObject Obje = Instantiate(Cover, new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                            Lanv.Add(new Vector3(x + Vk.x, y, z + Vk.z));
                            Lanw.Add(new Vector3(x + Vk.x, y, z + Vk.z));
                            Water.Add(Obje);
                        }
                        else
                        {
                            GameObject Obje = Instantiate(Cover, new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                            Block.Add(Obje);
                            Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));

                            int a = Random.Range(0, 60);

                            if (a == 0)
                            {
                                Log_Structure(new Vector3(x + Vk.x, y, z + Vk.z));
                            }
                            if (a > 40)
                            {
                                GameObject Obje3 = Instantiate(data.Grass[0], new Vector3(x + Vk.x, y + 1, z + Vk.z), Quaternion.identity, Content.transform);
                                Grass.Add(Obje3);
                                Lands.Add(new Land(false, 1, x + Vk.x, y + 1, z + Vk.z));
                            }
                        }
                    }
                    else if (y == 0)
                    {
                        GameObject Obje = Instantiate(data.Blocks[7], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));

                    }
                    else if (y < 3)
                    {
                        GameObject Obje = Instantiate(data.Blocks[2], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));

                    }
                    else
                    {
                        GameObject Obje = Instantiate(data.Blocks[1], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                    }


                }
            }
        }

        FindBehind();
    }

    public void Create()
    {
        aoi = true;
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                for (int y = (int)SizeReel; y < SizeY; y++)
                {
                    Vector3 Vk = gameObject.transform.position;
                    Lanv.Add(new Vector3(x + Vk.x, y, z + Vk.z));
                    Lands.Add(new Land(true, 0, x, y, z));
                }
            }
        }
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                for (int y = 0; y < SizeReel; y++)
                {
                    Vector3 Vk = gameObject.transform.position;
                    if(y == SizeReel - 1)
                    {
                        GameObject Obje = Instantiate(data.Blocks[0], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));

                        /*
                        int a = Random.Range(0, 60);

                        int Seedf = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / td, (z + Vk.z + seed) / td) * 36);
                        //Debug.Log(Seedf);

                        if (Seedf == 25 || Seedf == 7)
                        {
                            Log_Structure(new Vector3(x + Vk.x, y, z + Vk.z));
                        }
                        */
                    }
                    else if (y == 0)
                    {
                        GameObject Obje = Instantiate(data.Blocks[7], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));

                    }
                    else if (y < 3)
                    {
                        GameObject Obje = Instantiate(data.Blocks[2], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));

                    }
                    else
                    {
                        GameObject Obje = Instantiate(data.Blocks[1], new Vector3(x + Vk.x, y, z + Vk.z), Quaternion.identity, Content.transform);
                        Block.Add(Obje);
                        Lands.Add(new Land(false, 1, x + Vk.x, y, z + Vk.z));
                    }
                    
                    
                }
            }
        }

        Cr.men.Load_Chunk(transform.position, gameObject, ChunkNumber);
        FindBehind();        
    }

    public void Find(Vector3 Veck,bool Bo)
    {
        for (int i = 0; i < Lanv.Count; i++)
        {
            if(Lanv[i] == Veck)
            {
                Bo = true;
            }
            else
            {
                Bo = false;
            }
        }
    }

    public void Optimize_start()
    {
        

        foreach (GameObject block in Block)
        {
            if (block.tag == "Block")
            {
                block.transform.GetChild(0).gameObject.SetActive(false);
                block.transform.GetChild(1).gameObject.SetActive(false);
                block.transform.GetChild(2).gameObject.SetActive(false);
                block.transform.GetChild(3).gameObject.SetActive(false);
                block.transform.GetChild(4).gameObject.SetActive(false);
                block.transform.GetChild(5).gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < Block.Count; i++)
        {
            GameObject BlockV = Block[i];
            Vector3 Block_Vektor = BlockV.transform.position;

            if (BlockV.tag == "Block")
            {
                foreach (Vector3 Block_2 in Lanb)
                {
                    Vector3 Block_Vektor_2 = Block_2;

                    if (Block_Vektor.x + 1 == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(2).gameObject.SetActive(true);
                    }

                    if (Block_Vektor.x - 1 == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(3).gameObject.SetActive(true);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z - 1 == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(1).gameObject.SetActive(true);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z + 1 == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(0).gameObject.SetActive(true);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y + 1 == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(4).gameObject.SetActive(true);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y - 1 == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(5).gameObject.SetActive(true);
                    }

                }
            }
        }
    }

    public void Water_Optimize()
    {


        foreach (GameObject block in Water)
        {
            if (block.tag == "Water")
            {
                block.transform.GetChild(0).gameObject.SetActive(true);
                block.transform.GetChild(1).gameObject.SetActive(true);
                block.transform.GetChild(2).gameObject.SetActive(true);
                block.transform.GetChild(3).gameObject.SetActive(true);
                block.transform.GetChild(4).gameObject.SetActive(true);
                block.transform.GetChild(5).gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < Water.Count; i++)
        {
            GameObject BlockV = Water[i];
            Vector3 Block_Vektor = BlockV.transform.position;

            if (BlockV.tag == "Water")
            {
                foreach (Vector3 Block_2 in LanW)
                {
                    Vector3 Block_Vektor_2 = Block_2;

                    if (Block_Vektor.x + 1 == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(2).gameObject.SetActive(false);
                    }

                    if (Block_Vektor.x - 1 == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(3).gameObject.SetActive(false);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z - 1 == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(1).gameObject.SetActive(false);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z + 1 == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(0).gameObject.SetActive(false);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y + 1 == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(4).gameObject.SetActive(false);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y - 1 == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(5).gameObject.SetActive(false);
                    }

                }
            }
        }
    }

    public void Glass_Optimize()
    {


        foreach (GameObject block in Glass)
        {
            if (block.tag == "Grass")
            {
                block.transform.GetChild(0).gameObject.SetActive(true);
                block.transform.GetChild(1).gameObject.SetActive(true);
                block.transform.GetChild(2).gameObject.SetActive(true);
                block.transform.GetChild(3).gameObject.SetActive(true);
                block.transform.GetChild(4).gameObject.SetActive(true);
                block.transform.GetChild(5).gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < Glass.Count; i++)
        {
            GameObject BlockV = Glass[i];
            Vector3 Block_Vektor = BlockV.transform.position;

            if (BlockV.tag == "Grass")
            {
                foreach (Vector3 Block_2 in LanGG)
                {
                    Vector3 Block_Vektor_2 = Block_2;

                    if (Block_Vektor.x + 1 == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(2).gameObject.SetActive(false);
                    }

                    if (Block_Vektor.x - 1 == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(3).gameObject.SetActive(false);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z - 1 == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(1).gameObject.SetActive(false);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z + 1 == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(0).gameObject.SetActive(false);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y + 1 == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(4).gameObject.SetActive(false);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y - 1 == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.transform.GetChild(5).gameObject.SetActive(false);
                    }

                }
            }
        }
    }

    // Water


    public void Water_Pour(Vector3 Vk)
    {
        GameObject Obje = Instantiate(data.Water[1], Vk, Quaternion.identity, Content.transform);
        Lanw.Add(Vk);
        Lanwg.Add(Vk);
        Water.Add(Obje);
        FindBehind();
    }

    public void Water_Pour_Map(Vector3 Vk)
    {
        GameObject Obje = Instantiate(data.Water[1], Vk, Quaternion.identity, Content.transform);
        Lanw.Add(Vk);
        Lanwg.Add(Vk);
        Water.Add(Obje);
    }
    public void Water_Pour_toChunk(Vector3 Vk)
    {
        for (int G = 0; G < Behind.Count; G++)
        {
            GameObject chunk = Behind[G].Object;
            Chunk Chu = chunk.GetComponent<Chunk>();

            for (float i = chunk.transform.position.x; i < chunk.transform.position.x + 2; i++)
            {
                for (float j = chunk.transform.position.z; j < chunk.transform.position.z + 2; j++)
                {
                    GameObject Obje = Instantiate(data.Water[1], Vk, Quaternion.identity, Chu.Content.transform);
                    Chu.Lanw.Add(Vk);
                    Chu.Lanwg.Add(Vk);
                    Chu.Water.Add(Obje);
                    Chu.FindBehind();
                }
            }
        }

    }
    public void Water_Spread(Vector3 Vk)
    {
        int b = 0;
        for (int i = 0; i < Lanw.Count; i++)
        {
            if (Lanw[i] == Vk)
            {
                b++;
            }
        }

        if(b == 0)
        {
            GameObject Obje = Instantiate(data.Water[1], Vk, Quaternion.identity, Content.transform);
            Lanw.Add(Vk);
            Lanwg.Add(Vk);
            Water.Add(Obje);
            FindBehind();
        }
        
    }
    public void Water_Notice(Vector3 Vk)
    {

        int h = 0;
        for (int j = 0; j < LanW.Count; j++)
        {
            Vector3 Vb = LanW[j];
            if (Vk.x + 1 == Vb.x && Vk.z == Vb.z && Vk.y == Vb.y)
            {
                if (h == 0)
                {
                    Water_Spread(Vk);
                    Water_SC(Vk);
                }
                h++;
            }
            if (Vk.x - 1 == Vb.x && Vk.z == Vb.z && Vk.y == Vb.y)
            {
                if (h == 0)
                {
                    Water_Spread(Vk);
                    Water_SC(Vk);
                }
                h++;
            }
            if (Vk.z + 1 == Vb.z && Vk.x == Vb.x && Vk.y == Vb.y)
            {
                if (h == 0)
                {
                    Water_Spread(Vk);
                    Water_SC(Vk);
                }
                h++;
            }
            if (Vk.z - 1 == Vb.z && Vk.x == Vb.x && Vk.y == Vb.y)
            {
                if (h == 0)
                {
                    Water_Spread(Vk);
                    Water_SC(Vk);
                }
                h++;
            }
            if (Vk.z == Vb.z && Vk.x == Vb.x && Vk.y + 1 == Vb.y)
            {
                if (h == 0)
                {
                    Water_Spread(Vk);
                    Water_SC(Vk);
                }
                h++;
            }
        }
    }
    public void Water_SC(Vector3 Vk)
    {
        for (int i = 0; i < Behind.Count; i++)
        {
            Behind[i].Object.GetComponent<Chunk>().LanBehind();
        }
        Vector3 Vkv = new Vector3(0, 0, 0);
        for (int j = 0; j < Lanv.Count; j++)
        {
            Vector3 Vb = Lanv[j];
            if (Vk.x == Vb.x && Vk.z == Vb.z && Vk.y + 1 == Vb.y)
            {
                Vkv = Vb;
            }
            if (Vk.x + 1 == Vb.x && Vk.z == Vb.z && Vk.y == Vb.y)
            {
                Vks.Add(Vb);
            }
            if (Vk.x - 1 == Vb.x && Vk.z == Vb.z && Vk.y == Vb.y)
            {
                Vks.Add(Vb);
            }
            if (Vk.z + 1 == Vb.z && Vk.x == Vb.x && Vk.y == Vb.y)
            {
                Vks.Add(Vb);
            }
            if (Vk.z - 1 == Vb.z && Vk.x == Vb.x && Vk.y == Vb.y)
            {
                Vks.Add(Vb);
            }
            if (Vk.z == Vb.z && Vk.x == Vb.x && Vk.y - 1 == Vb.y)
            {
                //Debug.Log(Vb.x + " " + Vb.z);
                Vks.Add(Vb);
            }
        }
        for (int i = 0; i < LanW.Count; i++)
        {
            for (int y = 0; y < Vks.Count; y++)
            {
                if (LanW[i] == Vks[y])
                {
                    Debug.Log("Result : " + Vks[y] + " " + LanW[i]);
                    Vks.RemoveAt(y);
                }
            }
        }
        for (int i = 0; i < Vks.Count; i++)
        {
            Water_Spread(Vks[i]);
        }
        FindBehind();
        if(Vks.Count != 0)
        {
            time = true;
        }
    }
    public void Water_Delete(Vector3 Vk)
    {
        for (int i = 0; i < Water.Count; i++)
        {
            if(Water[i].transform.position == Vk)
            {
                Lanw.Remove(Vk);
                Lanwg.Remove(Vk);
                Destroy(Water[i]);
                Water.RemoveAt(i);
                FindBehind();
            }
        }
    }
    public void Optimize_start2()
    {
        foreach (GameObject block in Block)
        {
            if (block.tag == "Block")
                block.SetActive(false);
        }

        for (int i = 0; i < Block.Count; i++)
        {
            GameObject BlockV = Block[i];
            Vector3 Block_Vektor = BlockV.transform.position;

            if (BlockV.tag == "Block")
            {

                for (int j = 0; j < Lanb.Count; j++)
                {
                    Vector3 Block_Vektor_2 = Lanb[j];
                    if (Block_Vektor.x + 1 == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.SetActive(true);
                    }

                    if (Block_Vektor.x - 1 == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.SetActive(true);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z - 1 == Block_Vektor_2.z)
                    {
                        BlockV.SetActive(true);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y == Block_Vektor_2.y && Block_Vektor.z + 1 == Block_Vektor_2.z)
                    {
                        BlockV.SetActive(true);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y + 1 == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.SetActive(true);
                    }

                    if (Block_Vektor.x == Block_Vektor_2.x && Block_Vektor.y - 1 == Block_Vektor_2.y && Block_Vektor.z == Block_Vektor_2.z)
                    {
                        BlockV.SetActive(true);
                    }

                }
            }
        }
    }

    public void FindBehind()
    {
        Behind.Clear();
        Lanb.Clear();
        LanW.Clear();
        LanGG.Clear();
        LanwG.Clear();

        Behind.Add(Chu);
        for (int x = 0; x < Cr.ChunkData.Count; x++)
        {
            if(Chu.x + 1 == Cr.ChunkData[x].x && Chu.z == Cr.ChunkData[x].z)
            {
                Behind.Add(Cr.ChunkData[x]);
            }
            if (Chu.x + 1 == Cr.ChunkData[x].x && Chu.z + 1 == Cr.ChunkData[x].z)
            {
                Behind.Add(Cr.ChunkData[x]);
            }
            if (Chu.x == Cr.ChunkData[x].x && Chu.z + 1 == Cr.ChunkData[x].z)
            {
                Behind.Add(Cr.ChunkData[x]);
            }
            if (Chu.x + 1 == Cr.ChunkData[x].x && Chu.z - 1 == Cr.ChunkData[x].z)
            {
                Behind.Add(Cr.ChunkData[x]);
            }
            if (Chu.x - 1 == Cr.ChunkData[x].x && Chu.z + 1 == Cr.ChunkData[x].z)
            {
                Behind.Add(Cr.ChunkData[x]);
            }
            if (Chu.x - 1 == Cr.ChunkData[x].x && Chu.z - 1 == Cr.ChunkData[x].z)
            {
                Behind.Add(Cr.ChunkData[x]);
            }
            if (Chu.x == Cr.ChunkData[x].x && Chu.z - 1 == Cr.ChunkData[x].z)
            {
                Behind.Add(Cr.ChunkData[x]);
            }
            if (Chu.x - 1 == Cr.ChunkData[x].x && Chu.z == Cr.ChunkData[x].z)
            {
                Behind.Add(Cr.ChunkData[x]);
            }
        }


        for (int i = 0; i < Behind.Count; i++)
        {
            Behind[i].Object.GetComponent<Chunk>().LanBehind();
        }

        for (int i = 0; i < Behind.Count; i++)
        {
            Behind[i].Object.GetComponent<Chunk>().Optimize_start2();
            Behind[i].Object.GetComponent<Chunk>().Optimize_start();
            Behind[i].Object.GetComponent<Chunk>().Glass_Optimize();
            Behind[i].Object.GetComponent<Chunk>().Water_Optimize();
        }
    } 

    public void LanBehind()
    {
        Lanb.Clear();
        LanW.Clear();
        LanwG.Clear();
        LanGG.Clear();

        for (int x = 0; x < Behind.Count; x++)
        {
            Chunk a = Behind[x].Object.gameObject.GetComponent<Chunk>();
            //Debug.Log(Behind[x].Object.name);
            //Debug.Log(a.data.name + " " + x);

            for (int y = 0; y < a.Lanv.Count; y++)
            {
                Lanb.Add(a.Lanv[y]);
            }

            for (int y = 0; y < a.Lanw.Count; y++)
            {
                LanW.Add(a.Lanw[y]);
            }

            for (int y = 0; y < a.Lanwg.Count; y++)
            {
                LanwG.Add(a.Lanwg[y]);
            }

            for (int y = 0; y < a.Lang.Count; y++)
            {
                LanGG.Add(a.Lang[y]);
            }
        }
    }

    public void Log_Structure(Vector3 Vk)
    {
        Vk.y += 1;
        Block_Add(data.Blocks[3], new Vector3(Vk.x, Vk.y, Vk.z));

        Vk.y += 1;

        Block_Add(data.Blocks[3], new Vector3(Vk.x, Vk.y, Vk.z));

        for (int x = -1; x < 2; x++)
        {
            for (int y = 1; y < 3; y++)
            {
                for (int z = -1; z < 2; z++)
                {
                    if (y == 1 && z == 0 && x == 0)
                    {
                        Block_Add(data.Blocks[3], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                    }
                    else if (y == 2 && z == 0 && x == 0)
                    {
                        Block_Add(data.Blocks[3], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                    }
                    else if(y == 1)
                    {
                        Block_Add(data.Blocks[4], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                    }
                    else if(y == 2)
                    {
                        if(z == 0 || x == 0)
                        {
                            Block_Add(data.Blocks[4], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                        }
                        
                    }
                }
            }
        }

        Vk.y += 3;

        Block_Add(data.Blocks[4], new Vector3(Vk.x, Vk.y, Vk.z));

    }

    public void Birch_Log_Structure(Vector3 Vk)
    {
        Vk.y += 1;
        Block_Add(data.Blocks[23], new Vector3(Vk.x, Vk.y, Vk.z));

        Vk.y += 1;

        Block_Add(data.Blocks[23], new Vector3(Vk.x, Vk.y, Vk.z));

        for (int x = -1; x < 2; x++)
        {
            for (int y = 1; y < 3; y++)
            {
                for (int z = -1; z < 2; z++)
                {
                    if (y == 1 && z == 0 && x == 0)
                    {
                        Block_Add(data.Blocks[23], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                    }
                    else if (y == 2 && z == 0 && x == 0)
                    {
                        Block_Add(data.Blocks[23], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                    }
                    else if (y == 1)
                    {
                        Block_Add(data.Blocks[22], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                    }
                    else if (y == 2)
                    {
                        if (z == 0 || x == 0)
                        {
                            Block_Add(data.Blocks[22], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                        }

                    }
                }
            }
        }

        Vk.y += 3;

        Block_Add(data.Blocks[22], new Vector3(Vk.x, Vk.y, Vk.z));

    }

    public void Pine_Log_Structure(Vector3 Vk)
    {
        Vk.y += 1;
        GameObject Obje = Instantiate(data.Blocks[58], new Vector3(Vk.x, Vk.y, Vk.z), Quaternion.identity, Content.transform);
        Block.Add(Obje);
        Lands.Add(new Land(false, 1, Vk.x, Vk.y, Vk.z));
        Lanv.Remove(new Vector3(Vk.x, Vk.y, Vk.z));

        Vk.y += 1;

        GameObject Obje2 = Instantiate(data.Blocks[58], new Vector3(Vk.x, Vk.y, Vk.z), Quaternion.identity, Content.transform);
        Block.Add(Obje2);
        Lands.Add(new Land(false, 1, Vk.x, Vk.y, Vk.z));
        Lanv.Remove(new Vector3(Vk.x, Vk.y, Vk.z));


        for (int x = -1; x < 2; x++)
        {
            for (int y = 1; y < 4; y++)
            {
                for (int z = -1; z < 2; z++)
                {
                    if (y == 1 && z == 0 && x == 0)
                    {
                        GameObject Obje3 = Instantiate(data.Blocks[58], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z), Quaternion.identity, Content.transform);
                        Block.Add(Obje3);
                        Lands.Add(new Land(false, 1, Vk.x + x, Vk.y + y, Vk.z + z));
                        Lanv.Remove(new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                    }
                    else if (y == 2 && z == 0 && x == 0)
                    {
                        GameObject Obje3 = Instantiate(data.Blocks[58], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z), Quaternion.identity, Content.transform);
                        Block.Add(Obje3);
                        Lands.Add(new Land(false, 1, Vk.x + x, Vk.y + y, Vk.z + z));
                        Lanv.Remove(new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                    }
                    else if (y == 1)
                    {
                        GameObject Obje3 = Instantiate(data.Blocks[57], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z), Quaternion.identity, Content.transform);
                        Block.Add(Obje3);
                        Lands.Add(new Land(false, 1, Vk.x + x, Vk.y + y, Vk.z + z));
                        Lanv.Remove(new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                    }
                    else if (y > 1)
                    {
                        if (z == 0 || x == 0)
                        {
                            GameObject Obje3 = Instantiate(data.Blocks[57], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z), Quaternion.identity, Content.transform);
                            Block.Add(Obje3);
                            Lands.Add(new Land(false, 1, Vk.x + x, Vk.y + y, Vk.z + z));
                            Lanv.Remove(new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                        }

                    }
                }
            }
        }

        Vk.y += 4;

        Block_Add(data.Blocks[57], new Vector3(Vk.x, Vk.y, Vk.z));

    }

    public void Sakura_Log_Structure(Vector3 Vk)
    {
        Vk.y += 1;
        GameObject Obje = Instantiate(data.Blocks[60], new Vector3(Vk.x, Vk.y, Vk.z), Quaternion.identity, Content.transform);
        Block.Add(Obje);
        Lands.Add(new Land(false, 1, Vk.x, Vk.y, Vk.z));
        Lanv.Remove(new Vector3(Vk.x, Vk.y, Vk.z));

        Vk.y += 1;

        GameObject Obje2 = Instantiate(data.Blocks[60], new Vector3(Vk.x, Vk.y, Vk.z), Quaternion.identity, Content.transform);
        Block.Add(Obje2);
        Lands.Add(new Land(false, 1, Vk.x, Vk.y, Vk.z));
        Lanv.Remove(new Vector3(Vk.x, Vk.y, Vk.z));


        for (int x = -1; x < 2; x++)
        {
            for (int y = 1; y < 3; y++)
            {
                for (int z = -1; z < 2; z++)
                {
                    if (y == 1 && z == 0 && x == 0)
                    {
                        GameObject Obje3 = Instantiate(data.Blocks[60], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z), Quaternion.identity, Content.transform);
                        Block.Add(Obje3);
                        Lands.Add(new Land(false, 1, Vk.x + x, Vk.y + y, Vk.z + z));
                        Lanv.Remove(new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                    }
                    else if (y == 2 && z == 0 && x == 0)
                    {
                        GameObject Obje3 = Instantiate(data.Blocks[60], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z), Quaternion.identity, Content.transform);
                        Block.Add(Obje3);
                        Lands.Add(new Land(false, 1, Vk.x + x, Vk.y + y, Vk.z + z));
                        Lanv.Remove(new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                    }
                    else if (y == 1)
                    {
                        GameObject Obje3 = Instantiate(data.Blocks[59], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z), Quaternion.identity, Content.transform);
                        Block.Add(Obje3);
                        Lands.Add(new Land(false, 1, Vk.x + x, Vk.y + y, Vk.z + z));
                        Lanv.Remove(new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                    }
                    else if (y == 2)
                    {
                        if (z == 0 || x == 0)
                        {
                            GameObject Obje3 = Instantiate(data.Blocks[59], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z), Quaternion.identity, Content.transform);
                            Block.Add(Obje3);
                            Lands.Add(new Land(false, 1, Vk.x + x, Vk.y + y, Vk.z + z));
                            Lanv.Remove(new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                        }

                    }
                }
            }
        }

        Vk.y += 3;

        GameObject Obje5 = Instantiate(data.Blocks[59], new Vector3(Vk.x, Vk.y, Vk.z), Quaternion.identity, Content.transform);
        Block.Add(Obje5);
        Lands.Add(new Land(false, 1, Vk.x, Vk.y, Vk.z));
        Lanv.Remove(new Vector3(Vk.x, Vk.y, Vk.z));

    }

    public void SnowLog_Structure(Vector3 Vk)
    {
        Vk.y += 1;
        GameObject Obje = Instantiate(data.Blocks[3], new Vector3(Vk.x, Vk.y, Vk.z), Quaternion.identity, Content.transform);
        Block.Add(Obje);
        Lands.Add(new Land(false, 1, Vk.x, Vk.y, Vk.z));
        Lanv.Remove(new Vector3(Vk.x, Vk.y, Vk.z));

        Vk.y += 1;

        GameObject Obje2 = Instantiate(data.Blocks[3], new Vector3(Vk.x, Vk.y, Vk.z), Quaternion.identity, Content.transform);
        Block.Add(Obje2);
        Lands.Add(new Land(false, 1, Vk.x, Vk.y, Vk.z));
        Lanv.Remove(new Vector3(Vk.x, Vk.y, Vk.z));

        for (int x = -1; x < 2; x++)
        {
            for (int y = 1; y < 3; y++)
            {
                for (int z = -1; z < 2; z++)
                {
                    GameObject Obje3 = Instantiate(data.Blocks[16], new Vector3(Vk.x + x, Vk.y + y, Vk.z + z), Quaternion.identity, Content.transform);
                    Block.Add(Obje3);
                    Lands.Add(new Land(false, 1, Vk.x + x, Vk.y + y, Vk.z + z));
                    Lanv.Remove(new Vector3(Vk.x + x, Vk.y + y, Vk.z + z));
                }
            }
        }

        Vk.y += 3;

        GameObject Obje5 = Instantiate(data.Blocks[16], new Vector3(Vk.x, Vk.y, Vk.z), Quaternion.identity, Content.transform);
        Block.Add(Obje5);
        Lands.Add(new Land(false, 1, Vk.x, Vk.y, Vk.z));
        Lanv.Remove(new Vector3(Vk.x, Vk.y, Vk.z));

    }

    public void Cactus_Structure(Vector3 Vk)
    {
        Vk.y += 1;
        Block_Add(data.Blocks[10], new Vector3(Vk.x, Vk.y, Vk.z));

        Vk.y += 1;
        Block_Add(data.Blocks[10], new Vector3(Vk.x, Vk.y, Vk.z));
    }

    public void Kofia_Structure(Vector3 Vk)
    {
        Vk.y += 1;
        GameObject Obje = Instantiate(data.Blocks[13], new Vector3(Vk.x, Vk.y, Vk.z), Quaternion.identity, Content.transform);
        Block.Add(Obje);
        Lands.Add(new Land(false, 1, Vk.x, Vk.y, Vk.z));
        Lanv.Remove(new Vector3(Vk.x, Vk.y, Vk.z));

        Vk.y += 1;

        GameObject Obje2 = Instantiate(data.Blocks[13], new Vector3(Vk.x, Vk.y, Vk.z), Quaternion.identity, Content.transform);
        Block.Add(Obje2);
        Lands.Add(new Land(false, 1, Vk.x, Vk.y, Vk.z));
        Lanv.Remove(new Vector3(Vk.x, Vk.y, Vk.z));


    }

    public void Chest_Structure(Vector3 Vk)
    {
        Vk.y += 1;
        GameObject Obje = Instantiate(data.Blocks[11], new Vector3(Vk.x, Vk.y, Vk.z), Quaternion.identity, Content.transform);
        Block.Add(Obje);
        Lands.Add(new Land(false, 1, Vk.x, Vk.y, Vk.z));
        Lanv.Remove(new Vector3(Vk.x, Vk.y, Vk.z));
    }

    public void Building_Structure(Vector3 Vk)
    {
        Vk.y += 1;

        for (int yy = 0; yy < 7; yy++)
        {
            GameObject Obje7 = Instantiate(data.Blocks[12], new Vector3(Vk.x, Vk.y + yy, Vk.z), Quaternion.identity, Content.transform);
            Block.Add(Obje7);
            Lands.Add(new Land(false, 1, Vk.x, Vk.y + yy, Vk.z));
            Lanv.Remove(new Vector3(Vk.x, Vk.y + yy, Vk.z));
        }

        Vk.x += 2;

        for (int yy = 0; yy < 7; yy++)
        {
            GameObject Obje7 = Instantiate(data.Blocks[12], new Vector3(Vk.x, Vk.y + yy, Vk.z), Quaternion.identity, Content.transform);
            Block.Add(Obje7);
            Lands.Add(new Land(false, 1, Vk.x, Vk.y + yy, Vk.z));
            Lanv.Remove(new Vector3(Vk.x, Vk.y + yy, Vk.z));
        }

        Vk.x -= 2;
        Vk.z += 2;

        for (int yy = 0; yy < 7; yy++)
        {
            GameObject Obje7 = Instantiate(data.Blocks[12], new Vector3(Vk.x, Vk.y + yy, Vk.z), Quaternion.identity, Content.transform);
            Block.Add(Obje7);
            Lands.Add(new Land(false, 1, Vk.x, Vk.y + yy, Vk.z));
            Lanv.Remove(new Vector3(Vk.x, Vk.y + yy, Vk.z));
        }

        Vk.x += 2;

        for (int yy = 0; yy < 7; yy++)
        {
            GameObject Obje7 = Instantiate(data.Blocks[12], new Vector3(Vk.x, Vk.y + yy, Vk.z), Quaternion.identity, Content.transform);
            Block.Add(Obje7);
            Lands.Add(new Land(false, 1, Vk.x, Vk.y + yy, Vk.z));
            Lanv.Remove(new Vector3(Vk.x, Vk.y + yy, Vk.z));
        }

    }

    // Add & Delete

    public void Block_Add(GameObject Blocks, Vector3 location)
    {
        GameObject Obje = Instantiate(Blocks, location, Quaternion.identity, Content.transform);
        Block.Add(Obje);
        Lands.Add(new Land(false, 1, location.x,location.y,location.z));
        Lanv.Remove(location);
    }


    [System.Serializable]
    public class Land
    {
        public float x, y, z;
        public bool Air;
        public int BlockID;

        public Land(bool air, int ID, float X, float Y, float Z)
        {
            x = X; 
            y = Y; 
            z = Z;
            Air = air;
            BlockID = ID;
        }
    }

    [System.Serializable]
    public class DestroyBlock
    {
        public float x, y, z;

        public DestroyBlock(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
        }
    }

    [System.Serializable]
    public class AddBlock
    {
        public float x, y, z;
        public SlotInfo data;

        public AddBlock(float X, float Y, float Z, SlotInfo datas)
        {
            x = X;
            y = Y;
            z = Z;
            data = datas;
        }
    }
}

