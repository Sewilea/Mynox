using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Inventory Inven;
    public SlotScript[] Chest_Slot;
    public GameObject Canvas;
    public InterfaceScript face;
    void Start()
    {
        face = GameObject.Find("_Script").GetComponent<InterfaceScript>();
        Inven = GameObject.Find("_Script").gameObject.GetComponent<Inventory>();
    }

    void Update()
    {

    }

    public bool Sandıktabirşeyvarmı()
    {
        for (int i = 0; i < 27; i++)
        {
            if (Chest_Slot[i].Amount > 0)
            {
                return true;
            }
        }
        return false;
    }
}
