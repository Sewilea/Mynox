using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Animator anim;
    InterfaceScript sc;
    Inventory Inven;
    public BlocksData data;
    public SaveObject Ob;
    GameObject Choose;
    RaycastHit hit;
    World Cr;
    Menu men;

    public float rTime;
    bool time;
    void Start()
    {
        Cr = GameObject.Find("World").gameObject.GetComponent<World>();
        sc = GameObject.Find("_Script").GetComponent<InterfaceScript>();
        Inven = GameObject.Find("_Script").GetComponent<Inventory>();
        men = GameObject.Find("_Script").GetComponent<Menu>();
        Choose = GameObject.Find("Choose").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (time)
        {
            rTime = rTime + 1 * Time.deltaTime;
            Ob.Break = rTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            time = false;
            rTime = 0;
            Ob.Break = rTime;
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, 5) && !sc.Escape.activeSelf)
        {
            if (hit.transform.tag == "Block" && !sc.Cursor.activeSelf)
            {
                bool RightMouse = false;
                Choose.SetActive(true);
                GameObject Block = hit.transform.gameObject;
                BlockInfo BloIn = Block.GetComponent<BlockInfo>();
                Ob.RaycastBlock = BloIn.slot;
                Choose.transform.position = Block.transform.position;
                GameObject Chunk = Block.transform.parent.gameObject.transform.parent.gameObject;
                //Debug.Log(Block.name);

                if(hit.transform.name == "Chest(Clone)")
                {
                    Chest Che = Block.GetComponent<Chest>();
                    Ob.chest = Che;
                }

                if(Input.GetMouseButtonDown(1) && hit.transform.name == "Chest(Clone)")
                {
                    Chest Che = Block.GetComponent<Chest>();
                    sc.Enva_Open();
                    Inven.Chest.SetActive(true);
                    Inven.Chest_Load();
                }

                if (Input.GetMouseButtonDown(1) && BloIn.slot.ID == 51 && BloIn.slot.TypeID == 1)
                {
                    RightMouse = true;
                    sc.Furnace_Open();

                }

                if (Input.GetMouseButtonDown(1) && BloIn.slot.ID == 54 && BloIn.slot.TypeID == 1 && Block.GetComponent<Berry>().Message)
                {
                    Inven.ItemEkle(data.info[111], 1);
                    sc.Object_Sound("Leaf(Clone)");
                    Vector3 Vk = hit.transform.position;

                    Chunk Chu = Chunk.GetComponent<Chunk>();
                    GameObject Block2 = Instantiate(data.Blocks[4], Vk, Quaternion.identity, null);
                    Chu.DestB_Add(new global::Chunk.DestroyBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z));
                    Chu.AddB_Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, data.info[110]));
                    Block2.transform.parent = Chunk.transform.GetChild(0);
                    Chu.Block.Add(Block2);
                    Chu.Block.Remove(Block);
                    Destroy(Block);
                    anim.Play("Hit");
                }

                if (Input.GetMouseButtonDown(1) && Ob.infos.TypeID == 4 && Ob.infos.ID == 2)
                {
                    Vector3 Vk = hit.transform.position + hit.normal;
                    Chunk Chu = Chunk.GetComponent<Chunk>();
                    anim.Play("Hit");
                    Chu.Water_Spread(Vk);
                    Chu.Water_SC(Vk);

                }

                if (Input.GetMouseButtonDown(1) && BloIn.slot.ID == 47 && BloIn.slot.TypeID == 1)
                {
                    RightMouse = true;
                    anim.Play("Hit");
                    sc.Com.SetActive(true);
                    sc.Block.Play();
                }

                if (Input.GetMouseButtonDown(1) && BloIn.slot.ID == 45 && BloIn.slot.TypeID == 1)
                {
                    RightMouse = true;
                    anim.Play("Hit");
                    Block.GetComponent<Inputs>().MessageChanged();
                    sc.Block.Play();
                }

                if (Input.GetMouseButtonDown(1) && BloIn.slot.ID == 46 && BloIn.slot.TypeID == 1)
                {
                    RightMouse = true;
                    anim.Play("Hit");
                    
                    sc.Block.Play();
                }

                if (Input.GetMouseButtonDown(1) && Ob.infos.UnderID == 12 && Ob.infos.ID == 4 && BloIn.slot.UnderID == 14)
                {
                    anim.Play("Hit");
                    Vector3 Vb = hit.transform.position;
                    Vector3 Vk = hit.transform.position + hit.normal;
                    if(Vb.y + 1 == Vk.y)
                    {
                        GameObject Block2 = Instantiate(data.Grow[0], Vk, Quaternion.identity, null);
                        Chunk.GetComponent<Chunk>().AddB_Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, data.info[61]));
                        Block2.transform.parent = Chunk.transform.GetChild(0);
                        Chunk.GetComponent<Chunk>().Grass.Add(Block2);
                        if (Ob.Mode == 0)
                        {
                            Ob.Script.Amount--;
                            if (Ob.Script.Amount <= 0)
                            {
                                Ob.Script.Inven.sıfırla(Ob.Script);
                            }
                        }
                    }
                }

                if (Input.GetMouseButtonDown(1) && Ob.infos.UnderID == 12 && Ob.infos.ID == 6 && BloIn.slot.UnderID == 14)
                {
                    anim.Play("Hit");
                    Vector3 Vb = hit.transform.position;
                    Vector3 Vk = hit.transform.position + hit.normal;
                    if (Vb.y + 1 == Vk.y)
                    {
                        GameObject Block2 = Instantiate(data.Grow[1], Vk, Quaternion.identity, null);
                        Chunk.GetComponent<Chunk>().AddB_Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, data.info[63]));
                        Block2.transform.parent = Chunk.transform.GetChild(0);
                        Chunk.GetComponent<Chunk>().Grass.Add(Block2);
                        if (Ob.Mode == 0)
                        {
                            Ob.Script.Amount--;
                            if (Ob.Script.Amount <= 0)
                            {
                                Ob.Script.Inven.sıfırla(Ob.Script);
                            }
                        }
                    }
                }

                if (Input.GetMouseButtonDown(1) && Ob.infos.UnderID == 12 && Ob.infos.ID == 7 && BloIn.slot.UnderID == 14)
                {
                    anim.Play("Hit");
                    Vector3 Vb = hit.transform.position;
                    Vector3 Vk = hit.transform.position + hit.normal;
                    if (Vb.y + 1 == Vk.y)
                    {
                        int a = Random.Range(0, 2);
                        GameObject Block2 = Instantiate(data.Grow[a], Vk, Quaternion.identity, null);
                        Chunk.GetComponent<Chunk>().AddB_Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, data.info[63]));
                        Block2.transform.parent = Chunk.transform.GetChild(0);
                        Chunk.GetComponent<Chunk>().Grass.Add(Block2);
                        if (Ob.Mode == 0)
                        {
                            Ob.Script.Amount--;
                            if (Ob.Script.Amount <= 0)
                            {
                                Ob.Script.Inven.sıfırla(Ob.Script);
                            }
                        }
                    }
                }

                if (Input.GetMouseButtonDown(1) && Ob.infos.UnderID == 2)
                {
                    sc.Drag.Play();
                    anim.Play("Hit");
                    Chunk Chu = Chunk.GetComponent<Chunk>();
                    if (BloIn.slot.ID == 8 || BloIn.slot.ID == 7)
                    {
                        GameObject Block2 = Instantiate(data.Blocks[32], Block.transform.position, Quaternion.identity, Chunk.transform.GetChild(0).transform);
                        Chu.DestB_Add(new global::Chunk.DestroyBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z));
                        Chu.AddB_Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, data.info[57]));
                        Chu.Block.Add(Block2);
                        Chu.Block.Remove(Block);
                        Destroy(Block);
                    }
                }

                // BloIn.slot.ID == 22 && BloIn.slot.TypeID == 1
                if (Input.GetMouseButtonDown(1) && (BloIn.slot.ID == 22 || BloIn.slot.ID == 41 || BloIn.slot.ID == 42 || BloIn.slot.ID == 43) && BloIn.slot.TypeID == 1)
                {
                    //sc.Drag.Play();
                    anim.Play("Hit");
                    Chunk Chu = Chunk.GetComponent<Chunk>();
                    if (Ob.infos.UnderID == 20 && (Ob.infos.ID == 18))
                    {
                        sc.Drag.Play();
                        GameObject Block2 = Instantiate(data.Blocks[62], Block.transform.position, Quaternion.identity, Chunk.transform.GetChild(0).transform);
                        Chu.DestB_Add(new global::Chunk.DestroyBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z));
                        Chu.AddB_Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, data.info[98]));
                        Chu.Block.Add(Block2);
                        Chu.Block.Remove(Block);
                        Destroy(Block);

                        if (Ob.Mode == 0)
                        {
                            Ob.Script.Amount--;
                            if (Ob.Script.Amount <= 0)
                            {
                                Ob.Script.Inven.sıfırla(Ob.Script);
                            }
                        }
                    }
                    if (Ob.infos.UnderID == 20 && Ob.infos.ID == 19)
                    {
                        sc.Drag.Play();
                        GameObject Block2 = Instantiate(data.Blocks[63], Block.transform.position, Quaternion.identity, Chunk.transform.GetChild(0).transform);
                        Chu.DestB_Add(new global::Chunk.DestroyBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z));
                        Chu.AddB_Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, data.info[97]));
                        Chu.Block.Add(Block2);
                        Chu.Block.Remove(Block);
                        Destroy(Block);

                        if (Ob.Mode == 0)
                        {
                            Ob.Script.Amount--;
                            if (Ob.Script.Amount <= 0)
                            {
                                Ob.Script.Inven.sıfırla(Ob.Script);
                            }
                        }
                    }
                    if (Ob.infos.UnderID == 20 && Ob.infos.ID == 21)
                    {
                        sc.Drag.Play();
                        GameObject Block2 = Instantiate(data.Blocks[61], Block.transform.position, Quaternion.identity, Chunk.transform.GetChild(0).transform);
                        Chu.DestB_Add(new global::Chunk.DestroyBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z));
                        Chu.AddB_Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, data.info[96]));
                        Chu.Block.Add(Block2);
                        Chu.Block.Remove(Block);
                        Destroy(Block);

                        if (Ob.Mode == 0)
                        {
                            Ob.Script.Amount--;
                            if (Ob.Script.Amount <= 0)
                            {
                                Ob.Script.Inven.sıfırla(Ob.Script);
                            }
                        }
                    }
                }

                //Creater Crr = hit.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Creater>();

                if (Input.GetMouseButtonDown(0) && Ob.Mode == 1)
                {
                    anim.Play("Hit");
                    Chunk Chu = Chunk.GetComponent<Chunk>();

                    for (int j = 0; j < Chu.Block.Count; j++)
                    {
                        if (Chu.Block[j] == Block)
                        {
                            BlockInfo infos = Block.GetComponent<BlockInfo>();

                            if (hit.transform.name == "Chest(Clone)")
                            {
                                Chest Che = Block.GetComponent<Chest>();
                                if (!Che.Sandıktabirşeyvarmı())
                                {
                                    Chu.DestB_Add(new global::Chunk.DestroyBlock(Block.transform.position.x, Block.transform.position.y, Block.transform.position.z));
                                    Choose.SetActive(false);
                                    Chu.Block.Remove(Block);
                                    Chu.Lanv.Add(Block.transform.position);
                                    sc.Object_Sound(Block.name);
                                    Destroy(Block);
                                    Chu.FindBehind();
                                    Chu.Water_Notice(Block.transform.position);
                                }
                            }
                            else
                            {
                                Chu.DestB_Add(new global::Chunk.DestroyBlock(Block.transform.position.x, Block.transform.position.y, Block.transform.position.z));
                                Choose.SetActive(false);
                                Chu.Block.Remove(Block);
                                Chu.Lanv.Add(Block.transform.position);
                                sc.Object_Sound(Block.name);
                                Destroy(Block);
                                Chu.FindBehind();
                                Chu.Water_Notice(Block.transform.position);
                            }

                           

                            SlotInfo info = Block.GetComponent<BlockInfo>().slot;
                        }
                    }
                }

                if (Input.GetMouseButton(0) && Ob.Mode == 0)
                {
                    time = true;
                    anim.Play("Hit");
                    float Break = 0;
                    if(Ob.infos.UnderID == 1 && BloIn.slot.UnderID == 6)
                    {
                        Break = 0.6f - Ob.infos.Hit_time;
                    }
                    else if (Ob.infos.UnderID == 3 && BloIn.slot.UnderID == 7)
                    {
                        Break = 0.6f - Ob.infos.Hit_time;
                    }
                    else if (Ob.infos.UnderID == 4 && BloIn.slot.UnderID == 8)
                    {
                        Break = 0.6f - Ob.infos.Hit_time;
                    }
                    else if (Ob.infos.UnderID == 15 && BloIn.slot.UnderID == 16)
                    {
                        Break = 0.6f - Ob.infos.Hit_time;
                    }
                    else
                    {
                        Break = 1f;
                    }

                    if (rTime > Break)
                    {
                        rTime = 0;
                        Ob.Break = rTime;
                        time = false;
                        Chunk Chu = Chunk.GetComponent<Chunk>();

                        for (int j = 0; j < Chu.Block.Count; j++)
                        {
                            if (Chu.Block[j] == Block)
                            {
                                BlockInfo infos = Block.GetComponent<BlockInfo>();

                                if (infos.slot.ID == 1 && infos.slot.TypeID == 1 && Ob.Mode == 0)
                                {

                                }
                                else
                                {
                                    bool b = true;
                                    if (hit.transform.name == "Chest(Clone)")
                                    {
                                        Chest Che = Block.GetComponent<Chest>();
                                        if (!Che.Sandıktabirşeyvarmı())
                                        {
                                            Chu.DestB_Add(new global::Chunk.DestroyBlock(Block.transform.position.x, Block.transform.position.y, Block.transform.position.z));
                                            Choose.SetActive(false);
                                            Chu.Block.Remove(Block);
                                            Chu.Lanv.Add(Block.transform.position);
                                            sc.Object_Sound(Block.name);
                                            Destroy(Block);
                                            Chu.FindBehind();
                                            Chu.Water_Notice(Block.transform.position);
                                        }
                                        else
                                        {
                                            b = false;
                                        }
                                    }
                                    else
                                    {
                                        Chu.DestB_Add(new global::Chunk.DestroyBlock(Block.transform.position.x, Block.transform.position.y, Block.transform.position.z));
                                        Choose.SetActive(false);
                                        Chu.Block.Remove(Block);
                                        Chu.Lanv.Add(Block.transform.position);
                                        sc.Object_Sound(Block.name);
                                        Destroy(Block);
                                        Chu.FindBehind();
                                        Chu.Water_Notice(Block.transform.position);
                                    }
                                        

                                    SlotInfo info = Block.GetComponent<BlockInfo>().slot;
                                    if (Ob.Mode == 0)
                                    {

                                        if(info.ID == 13 || info.ID == 24)
                                        {
                                            int a = Random.Range(0, 3);
                                            if(a == 0)
                                            {
                                                info = data.info[50];
                                            }
                                            
                                            if(Ob.infos.UnderID != 15)
                                            {
                                                b = false;
                                            }
                                        }
                                        if(info.ID == 20)
                                        {
                                            info = data.info[40];
                                        }
                                        if (info.ID == 10)
                                        {
                                            //info = data.info[44];
                                        }
                                        if (info.ID == 6)
                                        {
                                            //info = data.info[45];
                                        }
                                        if (info.ID == 27)
                                        {
                                            //info = data.info[46];
                                        }
                                        if (info.ID == 26)
                                        {
                                            //info = data.info[48];
                                        }
                                        if (info.ID == 32)
                                        {
                                            info = data.info[13];
                                        }

                                        if (b)
                                            Inven.ItemEkle(info, 1);
                                    }
                                }
                            }
                        }
                    }
                }
                                
                if (Input.GetMouseButtonDown(1) && Ob.Block != null && Ob.infos.TypeID < 3 && hit.transform.name != "Chest(Clone)" && !RightMouse)
                {
                    anim.Play("Hit");
                    Vector3 Vk = hit.transform.position + hit.normal;

                    if (Vk.y < 30)
                    {
                        if (Ob.infos.TypeID == 1)
                        {
                            GameObject Block2 = Instantiate(Ob.Block, Vk, Quaternion.identity, null);

                            for (int G = 0; G < Cr.Chunks.Count; G++)
                            {
                                GameObject chunk = Cr.Chunks[G];

                                for (float i = chunk.transform.position.x; i < chunk.transform.position.x + 2; i++)
                                {
                                    for (float j = chunk.transform.position.z; j < chunk.transform.position.z + 2; j++)
                                    {
                                        if (Block2.transform.position.x == i && Block2.transform.position.z == j)
                                        {
                                            chunk.GetComponent<Chunk>().AddB_Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, Ob.infos));
                                            Block2.transform.parent = chunk.transform.GetChild(0);
                                            Cr.Chunks[G].GetComponent<Chunk>().Block.Add(Block2);
                                            Cr.Chunks[G].GetComponent<Chunk>().Lanv.Remove(Block2.transform.position);
                                            
                                            Cr.Chunks[G].GetComponent<Chunk>().Water_Delete(Block2.transform.position);
                                            sc.Object_Sound(Block2.name);
                                            chunk.GetComponent<Chunk>().FindBehind();

                                            if (Ob.Mode == 0)
                                            {
                                                Ob.Script.Amount--;
                                                if (Ob.Script.Amount <= 0)
                                                {
                                                    Ob.Script.Inven.sıfırla(Ob.Script);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (Ob.infos.TypeID == 2)
                        {
                            GameObject Block2 = Instantiate(Ob.Block, Vk, Quaternion.identity, null);

                            for (int G = 0; G < Cr.Chunks.Count; G++)
                            {
                                GameObject chunk = Cr.Chunks[G];

                                for (float i = chunk.transform.position.x; i < chunk.transform.position.x + 2; i++)
                                {
                                    for (float j = chunk.transform.position.z; j < chunk.transform.position.z + 2; j++)
                                    {
                                        if (Block2.transform.position.x == i && Block2.transform.position.z == j)
                                        {
                                            chunk.GetComponent<Chunk>().AddB_Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, Ob.infos));
                                            Block2.transform.parent = chunk.transform.GetChild(0);
                                            Cr.Chunks[G].GetComponent<Chunk>().Water_Delete(Block2.transform.position);
                                            Cr.Chunks[G].GetComponent<Chunk>().Grass.Add(Block2);
                                            sc.Object_Sound(Block2.name);

                                            if (Ob.infos.UnderID == 20)
                                            {
                                                Cr.Chunks[G].GetComponent<Chunk>().Glass.Add(Block2);
                                                Cr.Chunks[G].GetComponent<Chunk>().Lang.Add(Block2.transform.position);
                                            }
                                            Cr.Chunks[G].GetComponent<Chunk>().FindBehind();

                                            if (Ob.Mode == 0)
                                            {
                                                Ob.Script.Amount--;
                                                if (Ob.Script.Amount <= 0)
                                                {
                                                    Ob.Script.Inven.sıfırla(Ob.Script);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }



                    /*
                    Chunk Chu = Chunk.GetComponent<Chunk>();

                    for (int j = 0; j < Chu.Block.Count; j++)
                    {
                        Vector3 Vk = Block2.transform.position;
                        Vector3 Vk2 = Chu.Block[j].transform.position;

                        if (Vk.x == Vk2.x && Vk.y == Vk2.y)
                        {
                            Block2.transform.parent = Chu.Block[j].transform;
                            Chu.Block.Add(Block2);
                            Chu.Lanv.Remove(Vk);
                        }
                    }*/
                }

            }

            if ((hit.transform.tag == "Grass" || hit.transform.tag == "Slab") && !sc.Cursor.activeSelf)
            {
                Choose.SetActive(true);
                GameObject Block = hit.transform.gameObject;
                BlockInfo BloIn = Block.GetComponent<BlockInfo>();
                Ob.RaycastBlock = BloIn.slot;
                Choose.transform.position = Block.transform.position;
                GameObject Chunk = Block.transform.parent.gameObject.transform.parent.gameObject;

                if (Input.GetMouseButtonDown(0))
                {
                    
                    Chunk Chu = Chunk.GetComponent<Chunk>();

                    for (int j = 0; j < Chu.Grass.Count; j++)
                    {
                        if (Chu.Grass[j] == Block)
                        {
                            Chu.DestB_Add(new global::Chunk.DestroyBlock(Block.transform.position.x, Block.transform.position.y, Block.transform.position.z));
                            Choose.SetActive(false);
                            Chu.Grass.Remove(Block);
                            Chu.Lanv.Add(Block.transform.position);
                            sc.Object_Sound(Block.name);
                            Destroy(Block);
                            Chu.Water_Notice(Block.transform.position);

                            if (BloIn.slot.UnderID == 20)
                            {
                                Chu.GetComponent<Chunk>().Glass.Remove(Block);
                                Chu.GetComponent<Chunk>().Lang.Remove(Block.transform.position);
                            }
                            Chu.GetComponent<Chunk>().FindBehind();
                            SlotInfo info = Block.GetComponent<BlockInfo>().slot;
                            if (Ob.Mode == 0 && BloIn.slot.ID != 1 && BloIn.slot.UnderID != 2)
                            {
                                Inven.ItemEkle(info, 1);
                            }
                            else if (BloIn.slot.UnderID == 2)
                            {
                                if (BloIn.slot.ID == 10)
                                {
                                    Grown Gro = Block.GetComponent<Grown>();
                                    if (Gro.Growt == 3)
                                    {
                                        Inven.ItemEkle(data.info[59], 2);
                                        Inven.ItemEkle(data.info[60], 1);
                                    }
                                }
                                if (BloIn.slot.ID == 11)
                                {
                                    Grown Gro = Block.GetComponent<Grown>();
                                    if (Gro.Growt == 3)
                                    {
                                        Inven.ItemEkle(data.info[62], 4);
                                    }
                                }
                            }
                            else if(BloIn.slot.ID == 1)
                            {
                                int a = Random.Range(0, 6);
                                if(a == 0)
                                {
                                    Inven.ItemEkle(data.info[59], 1);
                                }
                            }
                        }
                    }
                }
                if (Input.GetMouseButtonDown(1) && Ob.Block != null && Ob.infos.TypeID < 3)
                {
                    anim.Play("Hit");
                    Vector3 Vk = hit.transform.position + hit.normal;

                    if (Vk.y < 30)
                    {
                        if (Ob.infos.TypeID == 1)
                        {
                            GameObject Block2 = Instantiate(Ob.Block, Vk, Quaternion.identity, null);

                            for (int G = 0; G < Cr.Chunks.Count; G++)
                            {
                                GameObject chunk = Cr.Chunks[G];

                                for (float i = chunk.transform.position.x; i < chunk.transform.position.x + 2; i++)
                                {
                                    for (float j = chunk.transform.position.z; j < chunk.transform.position.z + 2; j++)
                                    {
                                        if (Block2.transform.position.x == i && Block2.transform.position.z == j)
                                        {
                                            chunk.GetComponent<Chunk>().AddB_Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, Ob.infos));
                                            Block2.transform.parent = chunk.transform.GetChild(0);
                                            Cr.Chunks[G].GetComponent<Chunk>().Block.Add(Block2);
                                            Cr.Chunks[G].GetComponent<Chunk>().Lanv.Remove(Block2.transform.position);
                                            Cr.Chunks[G].GetComponent<Chunk>().Water_Delete(Block2.transform.position);
                                            sc.Object_Sound(Block2.name);
                                            chunk.GetComponent<Chunk>().FindBehind();

                                            if (Ob.Mode == 0)
                                            {
                                                Ob.Script.Amount--;
                                                if (Ob.Script.Amount <= 0)
                                                {
                                                    Ob.Script.Inven.sıfırla(Ob.Script);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (Ob.infos.TypeID == 2)
                        {
                            GameObject Block2 = Instantiate(Ob.Block, Vk, Quaternion.identity, null);

                            for (int G = 0; G < Cr.Chunks.Count; G++)
                            {
                                GameObject chunk = Cr.Chunks[G];

                                for (float i = chunk.transform.position.x; i < chunk.transform.position.x + 2; i++)
                                {
                                    for (float j = chunk.transform.position.z; j < chunk.transform.position.z + 2; j++)
                                    {
                                        if (Block2.transform.position.x == i && Block2.transform.position.z == j)
                                        {
                                            chunk.GetComponent<Chunk>().AddB_Add(new global::Chunk.AddBlock(Block2.transform.position.x, Block2.transform.position.y, Block2.transform.position.z, Ob.infos));
                                            Block2.transform.parent = chunk.transform.GetChild(0);
                                            Cr.Chunks[G].GetComponent<Chunk>().Water_Delete(Block2.transform.position);
                                            Cr.Chunks[G].GetComponent<Chunk>().Grass.Add(Block2);

                                            if (Ob.infos.UnderID == 20)
                                            {
                                                Cr.Chunks[G].GetComponent<Chunk>().Glass.Add(Block2);
                                                Cr.Chunks[G].GetComponent<Chunk>().Lang.Add(Block2.transform.position);
                                            }

                                            Cr.Chunks[G].GetComponent<Chunk>().FindBehind();
                                            sc.Object_Sound(Block2.name);

                                            if (Ob.Mode == 0)
                                            {
                                                Ob.Script.Amount--;
                                                if (Ob.Script.Amount <= 0)
                                                {
                                                    Ob.Script.Inven.sıfırla(Ob.Script);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    /*
                    Chunk Chu = Chunk.GetComponent<Chunk>();

                    for (int j = 0; j < Chu.Block.Count; j++)
                    {
                        Vector3 Vk = Block2.transform.position;
                        Vector3 Vk2 = Chu.Block[j].transform.position;

                        if (Vk.x == Vk2.x && Vk.y == Vk2.y)
                        {
                            Block2.transform.parent = Chu.Block[j].transform;
                            Chu.Block.Add(Block2);
                            Chu.Lanv.Remove(Vk);
                        }
                    }*/
                }
            }

        }
        else
        {
            Choose.SetActive(false);
            Ob.RaycastBlock = Ob.Null;
        }
    }
}
