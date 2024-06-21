using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBroke : MonoBehaviour
{
    public GameObject[] Broke;
    public SaveObject Ob;
    public int gg;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Ob.Break == 0)
        {
            gg = 0;
        }

        if (Ob.Break > 0.7f)
        {
            gg = 4;
        }
        else if (Ob.Break > 0.5f)
        {
            gg = 3;
        }
        else if (Ob.Break > 0.3f)
        {
            gg = 2;
        }
        else if (Ob.Break > 0.1f)
        {
            gg = 1;
        }
        else
        {
            gg = 0;
        }

        if(gg == 0)
        {
            Broke[0].SetActive(false);
            Broke[1].SetActive(false);
            Broke[2].SetActive(false);
            Broke[3].SetActive(false);
        }
        if (gg == 1)
        {
            Broke[0].SetActive(true);
            Broke[1].SetActive(false);
            Broke[2].SetActive(false);
            Broke[3].SetActive(false);
        }
        if (gg == 2)
        {
            Broke[0].SetActive(false);
            Broke[1].SetActive(true);
            Broke[2].SetActive(false);
            Broke[3].SetActive(false);
        }
        if (gg == 3)
        {
            Broke[0].SetActive(false);
            Broke[1].SetActive(false);
            Broke[2].SetActive(true);
            Broke[3].SetActive(false);
        }
        if (gg == 4)
        {
            Broke[0].SetActive(false);
            Broke[1].SetActive(false);
            Broke[2].SetActive(false);
            Broke[3].SetActive(true);
        }
    }


}
