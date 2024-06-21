using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceScript : MonoBehaviour
{
    public GameObject info, Escape, Cursor, Walk;
    public GameObject info_own, info_pack;
    public AudioSource sc, click, sc2, Dirt, Stone, Log, Leaf, Wood, Block, Hurt, Craft, Drag, Eat, Inv;
    public KeyCode Code;
    PlayerCursor cursor;
    public GameObject Cons;
    public bool Esc, Con;
    public int _Scene, _Sce;
    public SaveObject Ob;
    public Menu Men;
    public BlocksData data;
    public GameObject Player;
    public Inventory Inven;
    public Image ima;
    public Sprite[] Sti;
    public GameObject Enva, Ins, Cre, Sur, Die, P1, P2, Com, Furn, B1, B2, Page1, Page2;
    public bool En, In;

    public Text text, text2;
    public string[] st;

    public Text CamV, ChuV, Level;
    public Slider CamS, ChuS;
    public int Easy;
    private void Start()
    {
        sc.Play();
        cursor = GameObject.Find("_Script").transform.gameObject.GetComponent<PlayerCursor>();

        if(_Scene == 0)
        {
            int a = Random.Range(0, st.Length);
            text.text = st[a];
            text2.text = st[a];
        }
        if(_Scene == 1)
            Level.text = Ob.info.Create[5];

        CamS.value = Ob.CameraSize;
        ChuS.value = Ob.ChunkSize;
    }

    private void Update()
    {
        CamV.text = CamS.value.ToString();
        ChuV.text = ChuS.value.ToString();
        Ob.CameraSize = (int)CamS.value;
        Ob.ChunkSize = (int)ChuS.value;

        if (_Sce == 0)
        {
            if(Input.GetKeyDown(KeyCode.P) && !Con)
            {
                Ob.View++;
                if(Ob.View > 1)
                {
                    Ob.View = 0;
                }

            }
            
            if (Input.GetKeyDown(KeyCode.E) && !Con)
            {
                if (Com.activeSelf)
                {
                    Com.SetActive(false);
                }
                else
                {
                    Enva_Open();
                    Inven.Chest.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.R) && !Con)
            {
                In = !In;
                Ins.SetActive(In);
            }

            if (Input.GetKeyDown(KeyCode.Home))
            {
                Con = !Con;
            }
            if (Cons != null)
            {
                Cons.SetActive(Con);
            }

            if (Input.GetKeyDown(Code) && !Con)
            {
                Esc = !Esc;
                Escape.SetActive(Esc);
                if (Esc)
                {
                    P1.SetActive(true);
                    P2.SetActive(false);
                }
            }
        }

        if (_Scene == 0)
            ima.sprite = Sti[Ob.Skin];
    }

    public void _start()
    {
        click.Play();
    }

    public void _quit()
    {
        Application.Quit();
        click.Play();
    }

    public void Object_Sound(string a)
    {
        if(a == "Grass(Clone)" || a == "Snow_Grass(Clone)" || a == "Dirt(Clone)" || a == "Sand(Clone)")
        {
            Dirt.Play();
        }
        else if (a == "Stone(Clone)" || a == "Coal_Ore(Clone)" || a == "Iron_Ore(Clone)" || a == "Bedrock(Clone)")
        {
            Stone.Play();
        }
        else if (a == "Log(Clone)" || a == "Birch_Log(Clone)" || a == "Sakura Log(Clone)")
        {
            Log.Play();
        }
        else if (a == "Wood(Clone)" || a == "Chest(Clone)")
        {
            Wood.Play();
        }
        else if (a == "Leaf(Clone)" || a == "Snow_Leaf(Clone)" || a == "Grass_1(Clone)" || a == "Cactus(Clone)" || a == "Sakura ha(Clone)")
        {
            Leaf.Play();
        }
        else if (a == "Kofia(Clone)" || a == "Bico(Clone)" || a == "Bedbico(Clone)" || a == "Input(Clone)" || a == "Output(Clone)")
        {
            Block.Play();
        }
        else 
        {
            Stone.Play();
        }
    }

    // info

    public void _Open(GameObject Obje)
    {
        Obje.SetActive(true);
        click.Play();
    }

    public void _Close(GameObject Obje)
    {
        Obje.SetActive(false);
        cursor.Change(0);
        click.Play();
    }

    public void _info_open()
    {
        info.SetActive(true);
        click.Play();
    }

    public void _info_close()
    {
        info.SetActive(false);
        cursor.Change(0);
        click.Play();
    }

    public void _info_own()
    {
        info_own.SetActive(true);
        info_pack.SetActive(false);
        click.Play();
    }

    public void _info_pack()
    {
        info_own.SetActive(false);
        info_pack.SetActive(true);
        click.Play();
    }

    public void SkinS(int a)
    {
        Ob.Skin += a;

        if(Ob.Skin > 4)
        {
            Ob.Skin = 0;
        }
        if (Ob.Skin < 0)
        {
            Ob.Skin = 4;
        }
    }

    public void Form_Change()
    {
        click.Play();
        Easy++;
        if (Easy > 2)
        {
            Easy = 0;
            Level.text = Ob.info.Create[6];
        }
        if (Easy == 1)
        {
            Level.text = Ob.info.Create[4];
        }
        if (Easy == 2)
        {
            Level.text = Ob.info.Create[5];
        }


    }

    public void Enva_Open()
    {
        En = !En;
        Enva.SetActive(En);
        Cre.SetActive(false);
        Sur.SetActive(false);
        Inven.Chest.SetActive(false);
        Furn.SetActive(false);
        B1.SetActive(true);
        B2.SetActive(true);
        Page1.SetActive(true);
        Page2.SetActive(false);
        if (Ob.Mode == 1)
        {
            Cre.SetActive(En);
        }
        if (Ob.Mode == 0)
        {
            Sur.SetActive(En);
        }
    }

    public void Furnace_Open()
    {
        En = !En;
        Enva.SetActive(En);
        Cre.SetActive(false);
        Sur.SetActive(false);
        Inven.Chest.SetActive(false);
        Furn.SetActive(true);
        B1.SetActive(false);
        B2.SetActive(false);
        Page1.SetActive(false);
        Page2.SetActive(false);
        if (Ob.Mode == 1)
        {
            Cre.SetActive(En);
        }
        if (Ob.Mode == 0)
        {
            Sur.SetActive(En);
        }
    }


}
