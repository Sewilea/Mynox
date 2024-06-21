using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    InterfaceScript sc;
    Inventory inve;
    public Camera myCamera;
    public float MouseSensivity = 100f;
    public SaveObject Ob;

    public Transform PlayerBody;

    float xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        sc = GameObject.Find("_Script").GetComponent<InterfaceScript>();
        inve = GameObject.Find("_Script").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        myCamera.fieldOfView = Ob.CameraSize;
        if (!sc.Escape.activeSelf && !sc.Enva.activeSelf && !sc.Cons.activeSelf && !sc.Die.activeSelf && !sc.Com.activeSelf)
        {
            sc.Cursor.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            float mouseX = Input.GetAxis("Mouse X") * MouseSensivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            PlayerBody.Rotate(Vector3.up * mouseX);
        }
        else
        {
            sc.Cursor.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
