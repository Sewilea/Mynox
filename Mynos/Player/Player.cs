using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Front;
    public SaveObject Ob;
    public float XDifference, ZDifference;
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 My = gameObject.transform.position;
        Vector3 front = Front.transform.position;

        XDifference = My.x - front.x;
        ZDifference = My.z - front.z;

        if (ZDifference < -1 && XDifference > -1)
        {
            Ob.Message = "Front";
        }

        if (XDifference < -1 && ZDifference > -1)
        {
            Ob.Message = "Right";
        }

        if (ZDifference > 1 && XDifference < 1f)
        {
            Ob.Message = "Back";
        }

        if (XDifference > 1 && ZDifference < 1f)
        {
            Ob.Message = "Left";
        }

        /*
        if (Input.GetMouseButtonDown(0))
        {
            if(ZDifference < -1 && XDifference > -0.8f)
            {
                Debug.Log("Front");
            }

            if (XDifference < -1 && ZDifference > -0.8f)
            {
                Debug.Log("Right");
            }

            if (ZDifference > 1 && XDifference < 0.8f)
            {
                Debug.Log("Back");
            }

            if (XDifference > 1 && ZDifference < 0.8f)
            {
                Debug.Log("Left");
            }
        }
        */

        /*
        if(Ob.View == 0)
        {
            TPS.enabled = false;
            FPS.enabled = true;
            Body.SetActive(false);
            Arm.SetActive(true);
        }
        if (Ob.View == 1)
        {
            TPS.enabled = true;

            Body.SetActive(true);
            Arm.SetActive(false);
        }
        */
    }
}
