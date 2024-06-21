using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationsUI : MonoBehaviour
{
    public Sprite[] anim;
    public int x;
    public float hız;
    public bool hızRandom;

    public bool other;
    void Start()
    {

        Invoke("tekrarla", 0);
    }

    public void tekrarla()
    {
        if (other)
        {
            if (x == anim.Length)
            {
                x = 0;
            }

            gameObject.GetComponent<Image>().sprite = anim[x];
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

        if (x != 7 && !other)
        {
            if (x == anim.Length)
            {
                x = 0;
            }

            gameObject.GetComponent<Image>().sprite = anim[x];
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
