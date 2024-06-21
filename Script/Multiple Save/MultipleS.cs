using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleS : MonoBehaviour
{
    InterfaceScript sc;
    LoadLevel level;
    public List<GameObject> SaveA;
    public GameObject SaveP, Content, SaveB;
    public InputField field;
    public Text AT, BT, ST;
    public int Count;
    public SaveObject Obje;
    public Toggle Flat, Normal;
    public Slider Sli, Sli2;
    public Text text, text2, Mode, Norm;
    public int Modes, Normals;
    public LanScript Scc;
    public Sprite Latin, Katanaka;
    public Image Icon;

    void Start()
    {
        sc = GameObject.Find("_Script").GetComponent<InterfaceScript>();
        level = GameObject.Find("_Loading").GetComponent<LoadLevel>();
        Scc = GameObject.Find("Lan").GetComponent<LanScript>();
        Total_Load();

        Mode.text = Obje.info.Create[0];
        Norm.text = Obje.info.Create[2];
    }

    void Update()
    {
        if(Scc.info.Language_Number == 3)
        {
            Icon.sprite = Katanaka;
        }
        else
        {
            Icon.sprite = Latin;
        }

        //AT.text = "A : " + ai.ToString();
        //BT.text = "B : " + bi.ToString();
        //ST.text = si.ToString();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Total_Save();
        }
        Obje.Flats = Normals;
        if (Flat.isOn)
        {
            Normal.isOn = false;
            Obje.Flats = 0;
        }
        if (Normal.isOn)
        {
            Flat.isOn = false;
            Obje.Flats = 1;
        }
        Obje.Normal = Normal.isOn;
        Obje.Flat = Flat.isOn;
        text.text = Sli.value.ToString();
        text2.text = Sli2.value.ToString();
    }

    

    public void Plus()
    {
        if (field.text != "")
        {
            GameObject obje = Instantiate(SaveP, Content.transform);
            Content.GetComponent<RectTransform>().sizeDelta = new Vector2(Content.GetComponent<RectTransform>().sizeDelta.x, SaveA.Count * 70);
            PlayerPrefs.SetString("FS" + Count, field.text);

            SaveA.Add(obje);

            for (int i = 0; i < SaveA.Count; i++)
            {
                if (SaveA[i] == obje)
                {
                    obje.GetComponent<SaveInfo>().Number = Count;
                    Count++;
                }
            }
        }
    }

    public void Plus(int Count)
    {
        GameObject obje = Instantiate(SaveP, Content.transform);
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(Content.GetComponent<RectTransform>().sizeDelta.x, SaveA.Count * 70);
        SaveA.Add(obje);
        obje.GetComponent<SaveInfo>().Number = Count;
    }
    public void Save()
    {
        if (SaveB != null)
        {
            SaveInfo info = SaveB.GetComponent<SaveInfo>();
            int a = info.Number;
        }
    }

    public void Load()
    {
        if (SaveB != null)
        {
            SaveInfo info = SaveB.GetComponent<SaveInfo>();
            int a = info.Number;
        }
    }

    public void Total_Save()
    {
        for (int x = 0; x < SaveA.Count; x++)
        {
            PlayerPrefs.DeleteKey("FSA" + x);
            PlayerPrefs.DeleteKey("FS" + x);
        }
        int b = SaveA[SaveA.Count - 1].GetComponent<SaveInfo>().Number;
        PlayerPrefs.SetInt("FSL", b + 1);
        Debug.Log(b);
        for (int i = 0; i < b + 1; i++)
        {
            for (int j = 0; j < SaveA.Count; j++)
            {
                if (SaveA[j].GetComponent<SaveInfo>().Number == i)
                {
                    PlayerPrefs.SetInt("FSA" + i, 1);
                    PlayerPrefs.SetString("FS" + i, SaveA[j].GetComponent<SaveInfo>().Name.text);
                }
            }
        }
    }

    public void Total_Load()
    {

        for (int i = 0; i < PlayerPrefs.GetInt("FSL"); i++)
        {
            if (PlayerPrefs.GetInt("FSA" + i) == 1)
            {
                Plus(i);
            }
        }
        Count = PlayerPrefs.GetInt("FSL");
    }

    public void SeeS()
    {
        sc.click.Play();
        Obje.Number = Count - 1;
        Obje.Map = 0;
        Obje.isim = field.text;
        Obje.Value = (int)Sli.value;
        Obje.SizeY = (int)Sli2.value;
        Obje.Mode = Modes;
        level.Load_Level(2);
    }

    public void SeeSSave()
    {
        sc.click.Play();
        level.Load_Level(2);
    }

    public void Maps(int a)
    {
        sc.click.Play();
        Obje.Number = -1;
        Obje.Flats = 0;
        Obje.Value = 20;
        Obje.Map = a;
        level.Load_Level(2);
    }

    public void MapsO(MapObject Ob)
    {
        sc.click.Play();
        Obje.Number = -1;
        Obje.Flats = 0;
        Obje.Value = 20;
        Obje.Maps = Ob;
        Obje.Map = 4;
        level.Load_Level(2);
    }

    public void Mode_Change()
    {
        sc.click.Play();
        Modes++;
        if(Modes > 1)
        {
            Modes = 0;
            Mode.text = Obje.info.Create[0];
        }
        if (Modes == 1)
        {
            Mode.text = Obje.info.Create[1];
        }


    }

    public void Form_Change()
    {
        sc.click.Play();
        Normals++;
        if (Normals > 1)
        {
            Normals = 0;
            Norm.text = Obje.info.Create[3];
        }
        if (Normals == 1)
        {
            Norm.text = Obje.info.Create[2];
        }


    }

    public void LanButton()
    {
        Mode.text = Obje.info.Create[0];
        Norm.text = Obje.info.Create[2];
        Modes = 0;
        Normals = 1;

    }
}
