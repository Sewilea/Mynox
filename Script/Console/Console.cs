using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public LoadLevel load;
    public SaveObject Ob;
    public GameObject Massage, Content;
    public InputField field;
    public World wd;
    public BlocksData data;
    public Inventory Inven;
    GameObject Player;
    public string[] Command_Words;

    [Header("Nick")]
    string[] sesli_harf = { "o", "u", "i", "a", "e", "o", "u", "i", "a", "e" };
    string[] sessiz_harf = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "r", "s", "t", "v", "y", "z" , "w", "x" , "q"};

    int harfsayacı;
    int Sesli_harf;
    int Sessiz_harf;
    string isim;

    private void Start()
    {
        
    }
    public void run()
    {
        string Command = field.text;

        Command.TrimEnd();
        Command.ToLower();
        Command_Words = Command.Split(' ');

        if (Command_Words[0] == "/") // Komut yazılacak ise
        {
            if (Command_Words[1] == "nick") // Sahne ile ilgili
            {
                int harfsayısı = Random.Range(5, 10);

                isim = "";
                for (int i = 0; i < harfsayısı; i++)
                {
                    int a = Random.Range(0, 2);

                    if (a == 0)
                    {
                        isim = isim + sesli_harf[Random.Range(0, sesli_harf.Length)];
                    }
                    if (a == 1)
                    {
                        isim = isim + sessiz_harf[Random.Range(0, sessiz_harf.Length)];
                    }
                }
                Message(isim);

            }

            if (Command_Words[1] == "give") // Sahne ile ilgili
            {
                int id = int.Parse(Command_Words[2]);
                int typeid = int.Parse(Command_Words[3]);
                int amount = int.Parse(Command_Words[4]);
                Inven.ItemEkle(Inven.Find(id, typeid), amount);

            }

            if (Command_Words[1] == "gamemode") // Sahne ile ilgili
            {
                Ob.Mode = int.Parse(Command_Words[2]);
            }

            if (Command_Words[1] == "seed") // Sahne ile ilgili
            {
                Message(wd.seed + "");
            }

            if (Command_Words[1] == "scene") // Sahne ile ilgili
            {
                load.Load_Level(int.Parse(Command_Words[2]));
            }

            if (Command_Words[1] == "tp") // Sahne ile ilgili
            {
                Player = GameObject.FindGameObjectWithTag("Player").gameObject;
                Player.transform.position = new Vector3(int.Parse(Command_Words[2]), int.Parse(Command_Words[3]), int.Parse(Command_Words[4]));
            }

            if (Command_Words[1] == "save") // Kayıtla ilgili
            {
                if (Command_Words[2] == "delete") // silme
                {
                    PlayerPrefs.DeleteAll();
                    Message("All Save Deleted");
                }
            }

            if (Command_Words[1] == "print") // yazdırma
            {
                Message(Command_Words[2]);
            }
            /*
            if(Command_Words.Length > 2)
            {
                if (Command_Words[2] == "+") // toplama
                {
                    int a = int.Parse(Command_Words[1]);
                    int b = int.Parse(Command_Words[3]);

                    Message((a + b).ToString());
                }

                if (Command_Words[2] == "-") // çıkarma
                {
                    int a = int.Parse(Command_Words[1]);
                    int b = int.Parse(Command_Words[3]);

                    Message((a - b).ToString());
                }

                if (Command_Words[2] == "*") // çıkarma
                {
                    int a = int.Parse(Command_Words[1]);
                    int b = int.Parse(Command_Words[3]);

                    Message((a * b).ToString());
                }

                if (Command_Words[2] == "/") // çıkarma
                {
                    int a = int.Parse(Command_Words[1]);
                    int b = int.Parse(Command_Words[3]);

                    Message((a / b).ToString());
                }

                if (Command_Words[2] == "%") // çıkarma
                {
                    int a = int.Parse(Command_Words[1]);
                    int b = int.Parse(Command_Words[3]);

                    Message((a % b).ToString());
                }
            }
            */
        }
    }

    public void Message(string message)
    {
        GameObject Obje = Instantiate(Massage, Content.transform);
        Obje.transform.GetComponent<Text>().text = message;
        _Content();
    }

    public void _Content()
    {
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Content.transform.childCount * 100);
        // şükür
    }
}
