using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Alert_Text : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    PlayerCursor cursor;
    public string Name;
    public int NumberS;

    void Start()
    {
        cursor = GameObject.Find("_Script").transform.gameObject.GetComponent<PlayerCursor>();
    }

    void Update()
    {
        /*
        if (cursor.Dia != null)
        {
            if (cursor.Dia.activeSelf)
                cursor.TextP.SetActive(false);
        }
        */
        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        cursor.TextP.SetActive(true);

        cursor.textP.text = Name;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        cursor.TextP.SetActive(false);
    }
}
