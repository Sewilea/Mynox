using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Alert_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InterfaceScript sc;
    PlayerCursor cursor;

    public bool Click;
    void Start()
    {
        sc = GameObject.Find("_Script").GetComponent<InterfaceScript>();
        cursor = GameObject.Find("_Script").transform.gameObject.GetComponent<PlayerCursor>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //sc.click.Play();
        if (Click)
        {
            cursor.Change(1);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        cursor.Change(0);
    }
}
