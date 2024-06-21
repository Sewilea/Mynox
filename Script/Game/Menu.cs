using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public SaveObject Ob;
    public BlocksData data;
    public Text text, text2, text3, text4;
    public World world;
    public Inventory inven;
    public InterfaceScript face;
    public GameObject Die;
    GameObject Player;
    public GameObject Pla;
    public int Xy;
    public bool time, timeh;
    public float rTime, rTimeh;

    [Header("Gamemode")]
    public int Mode;

    [Header("Healt")]
    public int hea;
    public int maxhea;
    public GameObject[] henb, hunb;
    public int hun, maxhun;
    public Text texthea, texthun;

    [Header("Map")]
    public MapObject Map;

    [Header("Item_Name")]
    public Text texI;
    void Start()
    {
        face = GameObject.Find("_Script").GetComponent<InterfaceScript>();

        if (PlayerPrefs.GetInt("FSAS" + Ob.Number) == 1)
        {
            Load();
        }

    }

    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player").gameObject;
        text.text = "X : " + Player.transform.position.x + "\nY : " + Player.transform.position.y + "\nZ : " + Player.transform.position.z;
        text4.text = "Item Name : " + Ob.RaycastBlock.slotName;
        if (Ob.Mode == 0)
        {
            for (int i = 0; i < hunb.Length; i++)
            {
                hunb[i].SetActive(false);
            }
            int f = hun / 2;
            for (int i = 0; i < f; i++)
            {
                hunb[i].SetActive(true);
            }

            for (int i = 0; i < henb.Length; i++)
            {
                henb[i].SetActive(false);
            }
            int g = hea / 2;
            for (int i = 0; i < g; i++)
            {
                henb[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < henb.Length; i++)
            {
                henb[i].SetActive(false);
            }
            for (int i = 0; i < hunb.Length; i++)
            {
                hunb[i].SetActive(false);
            }
        }

        if(Ob.infos.ID != 0)
        {
            texI.text = Ob.infos.slotName;
        }
        else
        {
            texI.text = "";
        }

        if (time)
        {
            rTime = rTime + 1 * Time.deltaTime;
            if (rTime > 5)
            {
                hun--;
                rTime = 0;
            }
        }

        if (timeh)
        {
            rTimeh = rTimeh + 1 * Time.deltaTime;
            if (rTimeh > 2)
            {
                hea++;
                rTimeh = 0;
            }
        }

        if (hun == 20 && hea < 20)
        {
            timeh = true;
        }
        else
        {
            timeh = false;
            rTimeh = 0;
        }

        if(hea <= 0 || Player.transform.position.y < -20)
        {
            Die.SetActive(true);
        }

        OnPanel();
        if (Input.GetMouseButtonDown(1))
        {
            if(Ob.infos.UnderID == 10 && hun < 20)
            {
                face.Eat.Play();

                hun += 2;
                Ob.Script.Amount--;
                if (Ob.Script.Amount <= 0)
                {
                    Ob.Script.Inven.sıfırla(Ob.Script);
                }
            }
        }
        /*
        if(Ob.Number > 0)
        {
            Save();
        }
        
        if (Input.GetKeyDown(KeyCode.Z) && !face.Con)
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.X) && !face.Con)
        {
            Load_DestroyBlocks();
        }
        */

        /*
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Xy++;
            if(Xy == 10)
            {
                Xy = 0;
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Xy--;
            if (Xy < 0)
            {
                Xy = 9;
            }
        }
        text2.text = "Block : " + data.Blocks[Xy];
        */
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //MapSave();
        }

    }

    public void MapSave()
    {
        Map.value = Ob.Value;
        Map.CharX = Player.transform.position.x;
        Map.CharY = Player.transform.position.y;
        Map.CharZ = Player.transform.position.z;
        for (int i = 0; i < world.Chunks.Count; i++)
        {
            GameObject Obje = world.Chunks[i];
            Chunk Chu = Obje.GetComponent<Chunk>();
            List<MapBlock> MC = new List<MapBlock>();
            float x = Obje.transform.position.x;
            float y = Obje.transform.position.y;
            float z = Obje.transform.position.z;

            if (Chu.aoi)
            {
                for (int j = 0; j < Chu.Block.Count; j++)
                {
                    GameObject Obje2 = Chu.Block[j];
                    float B_x = Obje2.transform.position.x;
                    float B_y = Obje2.transform.position.y;
                    float B_z = Obje2.transform.position.z;
                    BlockInfo Info = Obje2.GetComponent<BlockInfo>();
                    int id = Info.slot.ID;
                    int typeid = Info.slot.TypeID;
                    MapBlock Block = new MapBlock(B_x, B_y, B_z, id, typeid);
                    MC.Add(Block);
                }
                for (int j = 0; j < Chu.Grass.Count; j++)
                {
                    GameObject Obje2 = Chu.Grass[j];
                    float B_x = Obje2.transform.position.x;
                    float B_y = Obje2.transform.position.y;
                    float B_z = Obje2.transform.position.z;
                    BlockInfo Info = Obje2.GetComponent<BlockInfo>();
                    int id = Info.slot.ID;
                    int typeid = Info.slot.TypeID;
                    MapBlock Block = new MapBlock(B_x, B_y, B_z, id, typeid);
                    MC.Add(Block);
                }
                for (int j = 0; j < Chu.Glass.Count; j++)
                {
                    GameObject Obje2 = Chu.Glass[j];
                    float B_x = Obje2.transform.position.x;
                    float B_y = Obje2.transform.position.y;
                    float B_z = Obje2.transform.position.z;
                    BlockInfo Info = Obje2.GetComponent<BlockInfo>();
                    int id = Info.slot.ID;
                    int typeid = Info.slot.TypeID;
                    MapBlock Block = new MapBlock(B_x, B_y, B_z, id, typeid);
                    MC.Add(Block);
                }
                for (int j = 0; j < Chu.Water.Count; j++)
                {
                    GameObject Obje2 = Chu.Water[j];
                    float B_x = Obje2.transform.position.x;
                    float B_y = Obje2.transform.position.y;
                    float B_z = Obje2.transform.position.z;
                    BlockInfo Info = Obje2.GetComponent<BlockInfo>();
                    int id = Info.slot.ID;
                    int typeid = Info.slot.TypeID;
                    MapBlock Block = new MapBlock(B_x, B_y, B_z, id, typeid);
                    MC.Add(Block);
                }
            }

            Map.Chunks.Add(new MapChunk(x, y, z, MC));
        }
    }

    public void MapLoadChunk(GameObject Chunk, Vector3 Vk, List<MapBlock> Blocks)
    {
        for (int i = 0; i < Blocks.Count; i++)
        {
            int id = Blocks[i].ID;
            int typeid = Blocks[i].TypeID;
            SlotInfo info = inven.Find(id, typeid);
            AddBlock(Chunk, new Vector3(Blocks[i].x, Blocks[i].y, Blocks[i].z), info);
        }
    }

    public void Save()
    {
        if (Ob.Number != -1)
        {
            PlayerPrefs.SetInt("FSAF" + Ob.Number, Ob.Flats);
            PlayerPrefs.SetInt("FSAY" + Ob.Number, Ob.SizeY);
            PlayerPrefs.SetInt("FSEED" + Ob.Number, world.seed);
            PlayerPrefs.SetInt("FSAV" + Ob.Number, Ob.Value);
            PlayerPrefs.SetInt("FSAM" + Ob.Number, Ob.Mode);
            PlayerPrefs.SetInt("FSAH" + Ob.Number, hea);
            PlayerPrefs.SetInt("FSAU" + Ob.Number, hun);
            PlayerPrefs.SetInt("FSA" + Ob.Number, 1);
            PlayerPrefs.SetInt("FSAS" + Ob.Number, 1);
            PlayerPrefs.SetString("FS" + Ob.Number, Ob.isim);
            PlayerPrefs.SetFloat("FS" + "X" + Ob.Number, Player.transform.position.x);
            PlayerPrefs.SetFloat("FS" + "Y" + Ob.Number, Player.transform.position.y);
            PlayerPrefs.SetFloat("FS" + "Z" + Ob.Number, Player.transform.position.z);

            for (int i = 0; i < inven.infos.Length; i++)
            {
                PlayerPrefs.SetInt("FS" + "PL" + "EN" + i.ToString() + "AMO" + Ob.Number, inven.infos[i].Amount);
                PlayerPrefs.SetFloat("FS" + "PL" + "EN" + i.ToString() + "ID" + Ob.Number, inven.infos[i].info.ID);
                PlayerPrefs.SetFloat("FS" + "PL" + "EN" + i.ToString() + "TYPEID" + Ob.Number, inven.infos[i].info.TypeID);
            }

            DestroyBlocks();
        }
    }

    public void DestroyBlocks()
    {
        for (int i = 0; i < world.ChunkData.Count; i++)
        {
            Chunk Chu = world.Chunks[i].GetComponent<Chunk>();
            if(Chu.DestB.Count != 0)
            {
                Vector3 Vk = world.Chunks[i].transform.position;
                PlayerPrefs.SetInt("FSA" + "DESCHU" + i + Ob.Number, 1);
                PlayerPrefs.SetInt("FSA" + "DESCHU" + i +"CO" + Ob.Number, Chu.DestB.Count);
                for (int j = 0; j < Chu.DestB.Count; j++)
                {
                    Vector3 Vb = new Vector3(Chu.DestB[j].x, Chu.DestB[j].y, Chu.DestB[j].z);
                    PlayerPrefs.SetInt("FSA" + "DESCHUB" + i + j + "X" + Ob.Number, (int)Vb.x);
                    PlayerPrefs.SetInt("FSA" + "DESCHUB" + i + j + "Y" + Ob.Number, (int)Vb.y);
                    PlayerPrefs.SetInt("FSA" + "DESCHUB" + i + j + "Z" + Ob.Number, (int)Vb.z);
                }
            }
            if (Chu.AddB.Count != 0)
            {
                Vector3 Vk = world.Chunks[i].transform.position;
                PlayerPrefs.SetInt("FSA" + "ADDCHU" + i + Ob.Number, 1);
                PlayerPrefs.SetInt("FSA" + "ADDCHU" + i + "CO" + Ob.Number, Chu.AddB.Count);
                for (int j = 0; j < Chu.AddB.Count; j++)
                {
                    Vector3 Vb = new Vector3(Chu.AddB[j].x, Chu.AddB[j].y, Chu.AddB[j].z);
                    //Debug.Log(Vb.x + " " + Vb.y + " " + Vb.z + "   " + Chu.ChunkNumber);
                    PlayerPrefs.SetInt("FSA" + "ADDCHUB" + i + j + "X" + Ob.Number, (int)Vb.x);
                    PlayerPrefs.SetInt("FSA" + "ADDCHUB" + i + j + "Y" + Ob.Number, (int)Vb.y);
                    PlayerPrefs.SetInt("FSA" + "ADDCHUB" + i + j + "Z" + Ob.Number, (int)Vb.z);
                    PlayerPrefs.SetInt("FSA" + "ADDCHUID" + i + j + "Z" + Ob.Number, Chu.AddB[j].data.ID);
                    PlayerPrefs.SetInt("FSA" + "ADDCHUTY" + i + j + "Z" + Ob.Number, Chu.AddB[j].data.TypeID);
                    if(Chu.AddB[j].data.ID == 4 && Chu.AddB[j].data.TypeID == 1)
                    {
                        for (int z = 0; z < Chu.Block.Count; z++)
                        {
                            if(Chu.Block[z].transform.position == Vb)
                            {
                                Chest Che = Chu.Block[z].GetComponent<Chest>();
                                for (int f = 0; f < Che.Chest_Slot.Length; f++)
                                {
                                    PlayerPrefs.SetInt("FS" + "ADDCHUB" + i + j + f + "AMO" + Ob.Number, Che.Chest_Slot[f].Amount);
                                    PlayerPrefs.SetInt("FS" + "ADDCHUB" + i + j + f + "ID" + Ob.Number, Che.Chest_Slot[f].info.ID);
                                    PlayerPrefs.SetInt("FS" + "ADDCHUB" + i + j + f + "TYPEID" + Ob.Number, Che.Chest_Slot[f].info.TypeID);
                                    Debug.Log(Che.Chest_Slot[f].Amount + " " + Che.Chest_Slot[f].info.ID + " " + Che.Chest_Slot[f].info.TypeID);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                PlayerPrefs.SetInt("FSA" + "ADDCHU" + i + Ob.Number, 0);
            }
            if (Chu.Lanwg.Count != 0)
            {
                PlayerPrefs.SetInt("FSA" + "WATCHU" + i + Ob.Number, 1);
                PlayerPrefs.SetInt("FSA" + "WATCHU" + i + "CO" + Ob.Number, Chu.Lanwg.Count);
                for (int j = 0; j < Chu.Lanwg.Count; j++)
                {
                    Vector3 Vb = new Vector3(Chu.Lanwg[j].x, Chu.Lanwg[j].y, Chu.Lanwg[j].z);
                    PlayerPrefs.SetInt("FSA" + "WATCHU" + i + j + "X" + Ob.Number, (int)Vb.x);
                    PlayerPrefs.SetInt("FSA" + "WATCHU" + i + j + "Y" + Ob.Number, (int)Vb.y);
                    PlayerPrefs.SetInt("FSA" + "WATCHU" + i + j + "Z" + Ob.Number, (int)Vb.z);
                }
            }
            else
            {
                PlayerPrefs.SetInt("FSA" + "WATCHU" + i + Ob.Number, 0);
            }
        }
    }

    public void Load()
    {
        Ob.Flats = PlayerPrefs.GetInt("FSAF" + Ob.Number);
        Ob.Mode = PlayerPrefs.GetInt("FSAM" + Ob.Number);
        hea = PlayerPrefs.GetInt("FSAH" + Ob.Number);
        hun = PlayerPrefs.GetInt("FSAU" + Ob.Number);
        Mode = Ob.Mode;
        for (int i = 0; i < inven.infos.Length; i++)
        {
            int miktar = PlayerPrefs.GetInt("FS" + "PL" + "EN" + i.ToString() + "AMO" + Ob.Number);
            float id = PlayerPrefs.GetFloat("FS" + "PL" + "EN" + i.ToString() + "ID" + Ob.Number);
            float typeid = PlayerPrefs.GetFloat("FS" + "PL" + "EN" + i.ToString() + "TYPEID" + Ob.Number);
            inven.ItemEkle(inven.Find(id, typeid), miktar);
        }
    }

    public void Load_DestroyBlocks()
    {
        for (int i = 0; i < world.ChunkData.Count; i++)
        {
            Chunk Chu = world.Chunks[i].GetComponent<Chunk>();
            if (PlayerPrefs.GetInt("FSA" + "DESCHU" + i + Ob.Number) == 1)
            {
                for (int j = 0; j < PlayerPrefs.GetInt("FSA" + "DESCHU" + i + "CO" + Ob.Number); j++)
                {
                    

                    Vector3 Vk = new Vector3(PlayerPrefs.GetInt("FSA" + "DESCHUB" + i + j + "X" + Ob.Number), PlayerPrefs.GetInt("FSA" + "DESCHUB" + i + j + "Y" + Ob.Number), PlayerPrefs.GetInt("FSA" + "DESCHUB" + i + j + "Z" + Ob.Number));
                    Debug.Log(Vk.x + " " + Vk.y + " " + Vk.z  + "   " + Chu.ChunkNumber);

                    for (int z = 0; z < Chu.Block.Count; z++)
                    {
                        GameObject Obje = Chu.Block[z];
                        if (Obje.transform.position == Vk)
                        {
                            Chu.DestB.Add(new global::Chunk.DestroyBlock(Obje.transform.position.x, Obje.transform.position.y, Obje.transform.position.z));
                            Chu.Block.Remove(Obje);
                            Chu.Lanv.Add(Obje.transform.position);
                            Destroy(Obje);
                            
                        }
                    }
                    for (int y = 0; y < Chu.Grass.Count; y++)
                    {
                        GameObject Obje2 = Chu.Grass[y];
                        if (Obje2.transform.position == Vk)
                        {
                            Chu.DestB.Add(new global::Chunk.DestroyBlock(Obje2.transform.position.x, Obje2.transform.position.y, Obje2.transform.position.z));
                            Chu.Grass.Remove(Obje2);
                            Chu.Lanv.Add(Obje2.transform.position);
                            Destroy(Obje2);
                        }
                    }
                }
            }
        }
    }

    public void Load_Chunk(Vector3 Vk, GameObject Obje, int Number)
    {
        int a = Number;
        int i = Number;
        Chunk Chu = Obje.GetComponent<Chunk>();

        if (PlayerPrefs.GetInt("FSA" + "DESCHU" + i + Ob.Number) == 1)
        {
            for (int j = 0; j < PlayerPrefs.GetInt("FSA" + "DESCHU" + i + "CO" + Ob.Number); j++)
            {

                Vector3 Vb = new Vector3(PlayerPrefs.GetInt("FSA" + "DESCHUB" + i + j + "X" + Ob.Number), PlayerPrefs.GetInt("FSA" + "DESCHUB" + i + j + "Y" + Ob.Number), PlayerPrefs.GetInt("FSA" + "DESCHUB" + i + j + "Z" + Ob.Number));
                //Debug.Log(Vb.x + " " + Vb.y + " " + Vb.z + "   " + a);
                for (int z = 0; z < Chu.Block.Count; z++)
                {
                    GameObject Obje3 = Chu.Block[z];
                    //Debug.Log(Obje3.transform.position.x + " " + Obje3.transform.position.y + " " + Obje3.transform.position.z + " " + Vb.x + " " + Vb.y + " " + Vb.z);
                    if (Obje3.transform.position == Vb)
                    {
                        Chu.DestB.Add(new global::Chunk.DestroyBlock(Obje3.transform.position.x, Obje3.transform.position.y, Obje3.transform.position.z));
                        Chu.Block.Remove(Obje3);
                        Chu.Lanv.Add(Obje3.transform.position);
                        Destroy(Obje3);

                    }
                }
                for (int y = 0; y < Chu.Grass.Count; y++)
                {
                    GameObject Obje2 = Chu.Grass[y];
                    if (Obje2.transform.position == Vb)
                    {
                        Chu.DestB.Add(new global::Chunk.DestroyBlock(Obje2.transform.position.x, Obje2.transform.position.y, Obje2.transform.position.z));
                        Chu.Grass.Remove(Obje2);
                        Chu.Lanv.Add(Obje2.transform.position);
                        Destroy(Obje2);
                    }
                }
            }
        }

        if (PlayerPrefs.GetInt("FSA" + "ADDCHU" + i + Ob.Number) == 1)
        {
            for (int j = 0; j < PlayerPrefs.GetInt("FSA" + "ADDCHU" + i + "CO" + Ob.Number); j++)
            {
                Vector3 Vb = new Vector3(PlayerPrefs.GetInt("FSA" + "ADDCHUB" + i + j + "X" + Ob.Number), PlayerPrefs.GetInt("FSA" + "ADDCHUB" + i + j + "Y" + Ob.Number), PlayerPrefs.GetInt("FSA" + "ADDCHUB" + i + j + "Z" + Ob.Number));
                //Debug.Log(Vb.x + " " + Vb.y + " " + Vb.z + "   " + a);
                SlotInfo info = inven.Find(PlayerPrefs.GetInt("FSA" + "ADDCHUID" + i + j + "Z" + Ob.Number), PlayerPrefs.GetInt("FSA" + "ADDCHUTY" + i + j + "Z" + Ob.Number));
                //Debug.Log(info.ID + " " + info.TypeID);
                if (info.TypeID == 1)
                {
                    GameObject Block2 = Instantiate(info.Block, Vb, Quaternion.identity, null);
                    Block2.transform.parent = Obje.transform.GetChild(0);
                    Chu.AddB.Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, info));
                    Chu.Block.Add(Block2);
                    Chu.Lanv.Remove(Block2.transform.position);

                    if(info.ID == 4)
                    {
                        for (int y = 0; y < 27; y++)
                        {
                            int id = PlayerPrefs.GetInt("FS" + "ADDCHUB" + i + j + y + "ID" + Ob.Number);
                            int typeid = PlayerPrefs.GetInt("FS" + "ADDCHUB" + i + j + y + "TYPEID" + Ob.Number);
                            SlotInfo infow = inven.Find(id, typeid);
                            Chest Che = Block2.GetComponent<Chest>();
                            Debug.Log(id + " " + typeid);
                            inven.ThisArrayItemEkle(infow, PlayerPrefs.GetInt("FS" + "ADDCHUB" + i + j + y + "AMO" + Ob.Number), Che.Chest_Slot);
                        }
                    }
                }
                if (info.TypeID == 2)
                {
                    GameObject Block2 = Instantiate(info.Block, Vb, Quaternion.identity, null);
                    Block2.transform.parent = Obje.transform.GetChild(0);
                    Chu.AddB.Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, info));

                    if(info.ID == 14)
                    {
                        Chu.Glass.Add(Block2);
                        Chu.Lang.Add(Block2.transform.position);
                    }
                    Chu.Grass.Add(Block2);
                }
            }
        }

        if (PlayerPrefs.GetInt("FSA" + "WATCHU" + i + Ob.Number) == 1)
        {
            for (int j = 0; j < PlayerPrefs.GetInt("FSA" + "WATCHU" + i + "CO" + Ob.Number); j++)
            {
                Vector3 Vb = new Vector3(PlayerPrefs.GetInt("FSA" + "WATCHU" + i + j + "X" + Ob.Number), PlayerPrefs.GetInt("FSA" + "WATCHU" + i + j + "Y" + Ob.Number), PlayerPrefs.GetInt("FSA" + "WATCHU" + i + j + "Z" + Ob.Number));
                Chu.Water_Pour(Vb);
            }
        }
    }

    public void Respawn()
    {
        hea = 20;
        Player.transform.position = new Vector3(Random.Range(Ob.Value / 2, Ob.Value * 2), 10, Random.Range(Ob.Value / 2, Ob.Value * 2));
        
    }

    public void OnPanel()
    {
        if(hun > 20)
        {
            hun = 20;
        }
        if (hun < 0)
        {
            hun = 0;
        }
        if (hea > 20)
        {
            hea = 20;
        }
        if (hea < 0)
        {
            hea = 0;
        }
    }

    public void AddBlock(GameObject Chunk,Vector3 Vk,SlotInfo info)
    {
        Chunk Chu = Chunk.GetComponent<Chunk>();
        if (info.TypeID == 1)
        {
            GameObject Block2 = Instantiate(info.Block, Vk, Quaternion.identity, null);
            Block2.transform.parent = Chunk.transform.GetChild(0);
            Chu.Block.Add(Block2);
            Chu.Lanv.Remove(Block2.transform.position);
        }
        if (info.TypeID == 2)
        {
            GameObject Block2 = Instantiate(info.Block, Vk, Quaternion.identity, null);
            Block2.transform.parent = Chunk.transform.GetChild(0);

            if (info.ID == 14)
            {
                Chu.Glass.Add(Block2);
                Chu.Lang.Add(Block2.transform.position);
            }
            Chu.Grass.Add(Block2);
        }
        if (info.TypeID == -1)
        {
            Chu.Water_Pour_Map(Vk);
        }
        
    }
}
