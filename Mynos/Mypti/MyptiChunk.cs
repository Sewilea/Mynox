using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyptiChunk : MonoBehaviour
{
    public GameObject chunk;
    public List<Mypti> myptis, myair;
    public List<Mypti> Behind;
    public GameObject EdgeX, EdgeY, EdgeZ;
    float SizeX = 8, SizeY = 32, SizeZ = 8;
    public int seed;
    public float terDetail;
    void Start()
    {
        Start_Formation_Noise();
    }

    void Update()
    {
        
    }

    public void Start_Formation()
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                for (int y = 8; y < SizeY; y++)
                {
                    Vector3 Vk = gameObject.transform.position;
                    myair.Add(new Mypti(gameObject,true, 0, x + Vk.x, y, z + Vk.z));

                }
            }
        }
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Vector3 Vk = gameObject.transform.position;
                    myptis.Add(new Mypti(gameObject, false, 1, x + Vk.x, y, z + Vk.z));
                }
            }
        }
        FindBehind();
    }

    public void Start_Formation_Noise()
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {

                Vector3 Vk = gameObject.transform.position;
                int maxY = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / terDetail, (z + Vk.z + seed) / terDetail) * 6);
                maxY += (int)6;

                for (int y = 0; y < SizeY; y++)
                {
                    if (y >= maxY)
                    {
                        myair.Add(new Mypti(gameObject, true, 0, x + Vk.x, y, z + Vk.z));
                    }
                }
            }
        }
        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                Vector3 Vk = gameObject.transform.position;
                int maxY = (int)(Mathf.PerlinNoise((x + Vk.x + seed) / terDetail, (z + Vk.z + seed) / terDetail) * 6);
                maxY += (int)6;

                for (int y = 0; y < maxY; y++)
                {
                    myptis.Add(new Mypti(gameObject, true, 0, x + Vk.x, y, z + Vk.z));
                }
            }
        }
        FindBehind();
    }

    public void CreateMesh()
    {
        for (int i = 0; i < myptis.Count; i++)
        {
            Mypti BlockV = myptis[i];

            for (int j = 0; j < myair.Count; j++)
            {
                Mypti BlockVT = myair[j];

                if (BlockV.x + 1 == BlockVT.x && BlockV.y == BlockVT.y && BlockV.z == BlockVT.z)
                {
                    Vector3 Position = new Vector3(BlockV.x + 0.5f, BlockV.y, BlockV.z);
                    Quaternion Rotation = EdgeX.transform.rotation;

                    GameObject Obje = Instantiate(EdgeX,Position,Rotation, gameObject.transform);
                }

                if (BlockV.x - 1 == BlockVT.x && BlockV.y == BlockVT.y && BlockV.z == BlockVT.z)
                {
                    Vector3 Position = new Vector3(BlockV.x - 0.5f, BlockV.y, BlockV.z);
                    Quaternion Rotation = EdgeX.transform.rotation;

                    GameObject Obje = Instantiate(EdgeX, Position, Rotation, gameObject.transform);
                }

                if (BlockV.x == BlockVT.x && BlockV.y == BlockVT.y && BlockV.z - 1 == BlockVT.z)
                {
                    Vector3 Position = new Vector3(BlockV.x, BlockV.y, BlockV.z - 0.5f);
                    Quaternion Rotation = EdgeZ.transform.rotation;

                    GameObject Obje = Instantiate(EdgeZ, Position, Rotation, gameObject.transform);
                }

                if (BlockV.x == BlockVT.x && BlockV.y == BlockVT.y && BlockV.z + 1 == BlockVT.z)
                {
                    Vector3 Position = new Vector3(BlockV.x, BlockV.y, BlockV.z + 0.5f);
                    Quaternion Rotation = EdgeZ.transform.rotation;

                    GameObject Obje = Instantiate(EdgeZ, Position, Rotation, gameObject.transform);
                }

                if (BlockV.x == BlockVT.x && BlockV.y + 1 == BlockVT.y && BlockV.z == BlockVT.z)
                {
                    Vector3 Position = new Vector3(BlockV.x, BlockV.y + 0.5f, BlockV.z);
                    Quaternion Rotation = EdgeY.transform.rotation;

                    GameObject Obje = Instantiate(EdgeY, Position, Rotation, gameObject.transform);
                }

                if (BlockV.x == BlockVT.x && BlockV.y - 1 == BlockVT.y && BlockV.z == BlockVT.z)
                {
                    Vector3 Position = new Vector3(BlockV.x, BlockV.y - 0.5f, BlockV.z);
                    Quaternion Rotation = EdgeY.transform.rotation;

                    GameObject Obje = Instantiate(EdgeY, Position, Rotation, gameObject.transform);
                }

            }
        }
    }

    public void LanBehind()
    {
        myair.Clear();
        for (int x = 0; x < Behind.Count; x++)
        {
            MyptiChunk a = Behind[x].Object.GetComponent<MyptiChunk>();
            //Debug.Log(Behind[x].Object.name);
            //Debug.Log(a.data.name + " " + x);

            for (int y = 0; y < a.myair.Count; y++)
            {
                myair.Add(a.myair[y]);
            }
        }
    }

    public void FindBehind()
    {
        Behind.Clear();

        Vector3 Vk = gameObject.transform.position;
        Behind.Add(new Mypti(gameObject, true, 0, Vk.x, Vk.y ,Vk.z));
        MyWord ww = chunk.GetComponent<MyWord>();

        for (int x = 0; x < ww.Timy.Count; x++)
        {
            if (Vk.x + 1 == ww.Timy[x].x && Vk.z == ww.Timy[x].z)
            {
                Behind.Add(new Mypti(gameObject, false, 0, ww.Timy[x].x, ww.Timy[x].y, ww.Timy[x].z));
            }
            if (Vk.x + 1 == ww.Timy[x].x && Vk.z + 1 == ww.Timy[x].z)
            {
                Behind.Add(new Mypti(gameObject, false, 0, ww.Timy[x].x, ww.Timy[x].y, ww.Timy[x].z));
            }
            if (Vk.x == ww.Timy[x].x && Vk.z + 1 == ww.Timy[x].z)
            {
                Behind.Add(new Mypti(gameObject, false,0, ww.Timy[x].x, ww.Timy[x].y, ww.Timy[x].z));
            }
            if (Vk.x + 1 == ww.Timy[x].x && Vk.z - 1 == ww.Timy[x].z)
            {
                Behind.Add(new Mypti(gameObject, false, 0, ww.Timy[x].x, ww.Timy[x].y, ww.Timy[x].z));
            }
            if (Vk.x - 1 == ww.Timy[x].x && Vk.z + 1 == ww.Timy[x].z)
            {
                Behind.Add(new Mypti(gameObject, false, 0, ww.Timy[x].x, ww.Timy[x].y, ww.Timy[x].z));
            }
            if (Vk.x - 1 == ww.Timy[x].x && Vk.z - 1 == ww.Timy[x].z)
            {
                Behind.Add(new Mypti(gameObject, false, 0, ww.Timy[x].x, ww.Timy[x].y, ww.Timy[x].z));
            }
            if (Vk.x == ww.Timy[x].x && Vk.z - 1 == ww.Timy[x].z)
            {
                Behind.Add(new Mypti(gameObject, false, 0, ww.Timy[x].x, ww.Timy[x].y, ww.Timy[x].z));
            }
            if (Vk.x - 1 == ww.Timy[x].x && Vk.z == ww.Timy[x].z)
            {
                Behind.Add(new Mypti(gameObject, false, 0, ww.Timy[x].x, ww.Timy[x].y, ww.Timy[x].z));
            }
        }


        for (int i = 0; i < Behind.Count; i++)
        {
            Behind[i].Object.GetComponent<MyptiChunk>().LanBehind();
        }

        for (int i = 0; i < Behind.Count; i++)
        {
            Behind[i].Object.GetComponent<MyptiChunk>().CreateMesh();
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
