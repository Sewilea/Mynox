using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanScript : MonoBehaviour
{
    public Text[] text, Parts;
    public SaveObject Ob;
    public LanguageInfo info;
    public MultipleS MulS;
    public LanguageInfo[] LanArray;
    public int _Scene;
    void Start()
    {
        if(PlayerPrefs.GetInt("FS" + "LA") != 0 && _Scene == 0)
        {
            for (int i = 0; i < LanArray.Length; i++)
            {
                if(LanArray[i].Language_Number == PlayerPrefs.GetInt("FS" + "LA"))
                {
                    TransA(LanArray[i]);
                }
            }
        }

        info = Ob.info;

        if(_Scene == 0)
        {
            Translate();
        }
        if (_Scene == 1)
        {
            Translate2();
        }

    }

    void Update()
    {
        
    }

    void Translate()
    {
        for (int j = 0; j < 3; j++)
        {
            Parts[j].text = info.Parts[j];
        }

        for (int i = 0; i < 4; i++)
        {
            text[i].text = info.Texts[i];
        }
        text[4].text = info.Texts[4];
        text[5].text = info.Texts[1];
        text[6].text = info.Texts[5];
        text[7].text = info.Texts[5];
        text[8].text = info.Texts[5];
        text[9].text = info.Texts[6];
        text[10].text = info.Texts[9];
        text[11].text = info.Texts[10];
        text[12].text = info.Texts[11];
        text[13].text = info.Texts[12];
        text[14].text = info.Texts[7];
        text[15].text = info.Texts[7];
        text[16].text = info.Texts[7];
        text[17].text = info.Texts[7];
        text[18].text = info.Texts[13];
        text[19].text = info.Texts[14];
        text[20].text = info.Texts[5];
        text[21].text = info.Texts[15];
        text[22].text = info.Games[8];
        text[23].text = info.Games[8];
        text[24].text = info.Games[10];
        text[25].text = info.Games[11];
        text[26].text = info.Games[12];
        text[27].text = info.Texts[16];
        text[28].text = info.Texts[17];
    }

    void Translate2()
    {
        for (int i = 0; i < 13; i++)
        {
            text[i].text = info.Games[i];
        }
        text[13].text = info.Texts[2];
        text[14].text = info.Texts[5];
    }

    public void TransA(LanguageInfo infos)
    {
        Ob.info = infos;
        info = infos;
        PlayerPrefs.SetInt("FS" + "LA", infos.Language_Number);
        Translate();
        MulS.LanButton();
    }

    public void TransB(LanguageInfo infos)
    {
        Ob.info = infos;
        info = infos;
        PlayerPrefs.SetInt("FS" + "LA", infos.Language_Number);
        Translate2();
    }
}
