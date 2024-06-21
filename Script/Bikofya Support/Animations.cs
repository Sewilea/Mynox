using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public Sprite[] anim;
    public int x;
    public float hız;
    public bool hızRandom;

    public bool start;
    void Start()
    {
        
        Invoke("tekrarla", 0);
    }

    public void tekrarla()
    {
        if(x == 2)
        {
            hız = 0.3f;
        }
        if(x != 11 && start)
        {

            if (x == anim.Length)
            {
                x = 0;
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = anim[x];
            x++;
            if (hızRandom)
            {
                Invoke("tekrarla", Random.Range(1, hız));
            }
            else
            {
                Invoke("tekrarla", hız);
            }
        }
        if (!start)
        {
            if (x == anim.Length)
            {
                x = 0;
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = anim[x];
            x++;
            if (hızRandom)
            {
                Invoke("tekrarla", Random.Range(1, hız));
            }
            else
            {
                Invoke("tekrarla", hız);
            }
        }
    }
}
