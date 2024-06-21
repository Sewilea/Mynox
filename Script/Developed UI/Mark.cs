using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mark : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    public GameObject Marks;


    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        
        if (Marks != null)
        {
            Marks.SetActive(true);
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {

    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Marks.SetActive(false);
    }
}
