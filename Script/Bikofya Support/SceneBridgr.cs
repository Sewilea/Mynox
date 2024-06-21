using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBridgr : MonoBehaviour
{
    public Animations anim;

    public float timer;
    public bool Timer;
    void Start()
    {
        
    }

    void Update()
    {
        if(anim.x == 8)
        {
            Timer = true;
        }

        if (Timer)
        {
            timer += 1 * Time.deltaTime;
        }

        if(timer > 1)
        {
            SceneManager.LoadScene(1);
        }
    }
}
