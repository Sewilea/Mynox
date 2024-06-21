using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : MonoBehaviour
{
    public SpriteRenderer[] Sprites;
    public Sprite Open, Close;
    public bool Message;
    public bool times = false;
    public float timei;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void MessageChanged(bool bol)
    {
        Message = bol;
        if (Message)
        {
            for (int i = 0; i < Sprites.Length; i++)
            {
                Sprites[i].sprite = Open;
            }
        }
        else
        {
            for (int i = 0; i < Sprites.Length; i++)
            {
                Sprites[i].sprite = Close;
            }
        }
    }
}
