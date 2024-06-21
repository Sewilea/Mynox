using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sheino : MonoBehaviour
{
    public Text text;
    public Button Ac, Re;
    public Text Act, Ret;

    [Header("Duygular")]
    public int com;
    public int OF, NE, UZ;
    public bool KA, RE;
    void Start()
    {
        Ac.interactable = false;
        Re.interactable = false;
    }

    void Update()
    {
        
    }

    public void _Close()
    {
        KA = false;
        RE = false;
        _Reset();
    }

    public void _Reset()
    {
        com = 0;
        _BReset();
    }

    public void _BReset()
    {
        KA = false;
        RE = false;
    }

    public void Offer()
    {
        com = -1;
        int a = 0; //Random.Range(0, 4);
        if(a == 0)
        {
            if(!RE && !KA)
            {
                Text("Bunu dinlemek istersin belki");
                Choose("Olur", "Olmaz");
            }
            if (KA)
            {
                Application.OpenURL("https://www.youtube.com/watch?v=SpJ96ZRj9dk");
                com--;
                KA = false;
            }
            if (RE)
            {
                com--;
                RE = false;
            }
        }
    }

    public void Chat()
    {
        Ac.interactable = true;
        Re.interactable = true;

        if (com == 0)
        {
            Text("Merhaba !");
            Choose("Nasılsın ?", "Kapat !");
        }

        else if (com == 2)
        {
            if (KA)
            {
                Text("İyi birine benziyosun !");
                Choose("Öyle derler", "Öyleyimdir");
                com++;
                KA = false;
            }
            if (RE)
            {
                Text("Aaa Neden ?");
                Choose("Hayat zor", "senden ötürü");
                com += 2;
                RE = false;
            }
        }
        
        else if (com == 3)
        {
            if (KA)
            {
                Text("Bu çok iyi bişey kaybetme !");
                Choose("", "");
                KA = false;
            }
            if (RE)
            {
                Text("Çok Egolusun, böyle insanları sevmem");
                Choose("", "");
                RE = false;
            }
        }
        
        else if (com > 4)
        {
            if (KA)
            {
                Text("Önemli Olan Savaşmaktır");
                Choose("", "");
                KA = false;
            }
            if (RE)
            {
                Text("Ben ne yaptım Pardon ?");
                Choose("Kapatalım konuyu", "Daha ne yapcan ?");
                RE = false;
            }
        }
    }

    // Shortcuts

    public void Text(string a)
    {
        text.text = a;
    }
    public void Choose(string a,string b)
    {
        if(a == "" && b == "")
        {
            Ac.interactable = false;
            Re.interactable = false;
        }
        else
        {
            Ac.interactable = true;
            Re.interactable = true;
        }

        Act.text = a;
        Ret.text = b;
    }

    // Buttons

    public void Accept()
    {
        KA = true;

        if(com == -2)
        {
            _Close();
        }
        if(com == -1)
        {
            Offer();
        }
        if (com == 0)
        {
            com++;
            Text("İyiyim gayet !");
            Choose("iyi iyi", "bana sormayacakmısın ?");
            NE++;
        }
        else if (com == 1)
        {
            Text("iyi diyelim iyi olsun, sen nasılsın ?");
            Choose("iyiyim", "kötüyüm");
            com++;
        }
        else if (com >= 2)
        {
            Chat();
        }
    }

    public void Refuce()
    {
        RE = true;
        if (com == -2)
        {
            _Close();
        }
        if (com == -1)
        {
            Offer();
        }
        if (com == 0)
        {
            Text("TAMAM !");
            Choose("", "");
        }
        else if (com == 1)
        {
            Text("Sen nasılsın ?");
            Choose("iyiyim", "kötüyüm");
            com++;
            OF++;
        }
        else if(com >= 2)
        {
            Chat();
        }
    }


}
