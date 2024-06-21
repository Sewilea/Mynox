using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grown : MonoBehaviour
{
    public SaveObject Ob;
    public BlocksData data;
    public SpriteRenderer[] Rend;
    public Sprite[] sprites;
    public bool GrowTime;
    public float Grow;
    public int Growt;
    void Start()
    {
        
    }
    void Update()
    {
        if (GrowTime)
        {
            Grow = Grow + 1 * Time.deltaTime;
        }
        if(Grow < 10)
        {
            Growt = 0;
            Rend[0].sprite = sprites[0];
            Rend[1].sprite = sprites[0];
            Rend[2].sprite = sprites[0];
            Rend[3].sprite = sprites[0];
        }
        else if (Grow < 20)
        {
            Growt = 2;
            Rend[0].sprite = sprites[1];
            Rend[1].sprite = sprites[1];
            Rend[2].sprite = sprites[1];
            Rend[3].sprite = sprites[1];
        }
        else if (Grow < 30)
        {
            Growt = 3;
            Rend[0].sprite = sprites[2];
            Rend[1].sprite = sprites[2];
            Rend[2].sprite = sprites[2];
            Rend[3].sprite = sprites[2];
        }
    }
}
