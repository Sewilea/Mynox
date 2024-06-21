using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class One : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Type;
    public string property;

    public Serino Ser;
    public Image Iam;

    void Start()
    {
        Ser = GameObject.Find("Serino").GetComponent<Serino>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Iam.color = new Color(0, 0, 0);
        Ser.Mouse = gameObject;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (Type == "")
        {
            Iam.color = new Color(255, 255, 255);
        }
        else
        {
            Iam.color = Ser.Black;
        }
    }
}
