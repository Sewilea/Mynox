using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveInfo : MonoBehaviour
{
    InterfaceScript sc;
    public MultipleS Multi;
    public LoadLevel level;
    public Text text, Name, Play, DeleteR;
    public LanScript Scc;

    private void Start()
    {
        sc = GameObject.Find("_Script").GetComponent<InterfaceScript>();
        Multi = GameObject.Find("_Script").GetComponent<MultipleS>();
        level = GameObject.Find("_Loading").GetComponent<LoadLevel>();
        Scc = GameObject.Find("Lan").GetComponent<LanScript>();
    }
    public int Number;
    private void Update()
    {
        Play.text = Scc.info.Texts[7];
        DeleteR.text = Scc.info.Texts[8];

        text.text = PlayerPrefs.GetString("FS" + Number);
    }

    public void See()
    {
        Multi.SaveB = gameObject;
        Multi.Load();
    }

    public void SeeS()
    {
        sc.click.Play();
        Multi.Total_Save();
        Multi.Obje.Map = 0;
        Multi.Obje.Number = Number;
        Multi.Obje.isim = text.text;
        Multi.Obje.SizeY = PlayerPrefs.GetInt("FSAY" + Number);
        Multi.Obje.Flats = PlayerPrefs.GetInt("FSAF" + Number);
        Multi.Obje.Value = PlayerPrefs.GetInt("FSAV" + Number);
        level.Load_Level(2);
    }

    public void Delete()
    {
        sc.click.Play();
        PlayerPrefs.DeleteKey("FSA" + Number);
        PlayerPrefs.DeleteKey("FS" + Number);
        Multi.SaveA.Remove(gameObject);
        Destroy(gameObject);
    }
}
