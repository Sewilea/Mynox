using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour
{
    InterfaceScript sc;
    public GameObject Dialogue, CloseB, Dialogue2, CloseB2;
    public Text DialogueT, DialogueT2, nameT;
    char[] letters;

    bool Timer;
    float timer;
    int X;

    public int _Scene;

    private void Start()
    {
        sc = GameObject.Find("_Script").GetComponent<InterfaceScript>();
        if (_Scene == 1)
        {
            Talk("This is the section for information.");
        }
        //Talk("Hello! Welcome to future !");
    }

    private void Update()
    {
        if (Timer)
        {
            timer += 1 * Time.deltaTime;
        } 
    }

    public void Write()
    {
        if(X < letters.Length)
        {
            DialogueT.text += letters[X].ToString();
            X++;
            sc.sc2.Play();
            Invoke("Write", 0.07f);
        }
        else
        {
            CloseB.SetActive(true);
        }
        
    }

    public void Talk(string a)
    {
        X = 0;
        DialogueT.text = "";
        CloseB.SetActive(false);
        letters = a.ToCharArray();
        Dialogue.SetActive(true);
        Invoke("Write", 0.07f);
    }   
}
