using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameinterfaceScript : MonoBehaviour
{
    public GameObject AraMenü;
    public bool AraMenüb;
    public AudioSource sc, click;

    [Header("Settings")]
    public KeyCode Menu_code;

    private void Start()
    {
        sc.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(Menu_code))
        {
            AraMenüb = !AraMenüb;
        }

        AraMenü.SetActive(AraMenüb);
    }

    public void _continue()
    {
        AraMenüb = !AraMenüb;
        click.Play();
    }

    public void _Back_to_menu()
    {
        click.Play();
    }

    public void _Click()
    {
        click.Play();
    }


}
