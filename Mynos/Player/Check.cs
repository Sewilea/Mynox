using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public PlayerMovement Mov;

    public void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Block")
        {
            Mov.isGrounded = true;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Block")
        {
            Mov.isGrounded = false;
        }
    }
}
