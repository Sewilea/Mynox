using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCursor : MonoBehaviour
{
    public Vector2 CursorPosition;

    public GameObject Cursors;
    public Sprite Idle, Click;
    //public CursorShape Color;

    public GameObject TextP;
    public Text textP;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CursorPosition = Input.mousePosition;

    }

    void Update()
    {
        CursorPosition = Input.mousePosition;

        Cursors.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void Change(int a)
    {
        
        if(a == 0)
        {
            Cursors.transform.gameObject.GetComponent<Image>().sprite = Idle;
        }
        if (a == 1)
        {
            Cursors.transform.gameObject.GetComponent<Image>().sprite = Click;
        }
        
    }

    public void Panel_Close()
    {
        TextP.SetActive(false);
    }
}
