using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    InterfaceScript sc;
    Inventory inve;
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float JumpHeight = 0.5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public SpriteRenderer[] Skin;


    Vector3 velocity;
    public bool isGrounded;

    public GameObject Cam;

    public SpriteRenderer ArmSp, TooSp;
    public GameObject ArmOb, TooOb;

    public SaveObject Ob;
    public GameObject Hand;

    public float rTime;
    public bool time;

    private void Start()
    {
        sc = GameObject.Find("_Script").GetComponent<InterfaceScript>();
        inve = GameObject.Find("_Script").GetComponent<Inventory>();

        Aspect();
    }

    public void Aspect()
    {
        if(Ob.Skin == 2)
        {
            Skin[0].sprite = Ob.Emo[1];
            Skin[1].sprite = Ob.Emo[2];
            Skin[2].sprite = Ob.Emo[0];
            Skin[3].sprite = Ob.Emo[0];
            Skin[4].sprite = Ob.Emo[0];
            Skin[5].sprite = Ob.Emo[0];
        }

        if (Ob.Skin == 1)
        {
            Skin[0].sprite = Ob.CanvorQi[1];
            Skin[1].sprite = Ob.CanvorQi[2];
            Skin[2].sprite = Ob.CanvorQi[0];
            Skin[3].sprite = Ob.CanvorQi[0];
            Skin[4].sprite = Ob.CanvorQi[0];
            Skin[5].sprite = Ob.CanvorQi[0];
        }

        if (Ob.Skin == 0)
        {
            Skin[0].sprite = Ob.Minox[1];
            Skin[1].sprite = Ob.Minox[2];
            Skin[2].sprite = Ob.Minox[0];
            Skin[3].sprite = Ob.Minox[0];
            Skin[4].sprite = Ob.Minox[0];
            Skin[5].sprite = Ob.Minox[0];
        }

        if (Ob.Skin == 3)
        {
            Skin[0].sprite = Ob.Serino[1];
            Skin[1].sprite = Ob.Serino[2];
            Skin[2].sprite = Ob.Serino[0];
            Skin[3].sprite = Ob.Serino[0];
            Skin[4].sprite = Ob.Serino[0];
            Skin[5].sprite = Ob.Serino[0];
        }

        if (Ob.Skin == 4)
        {
            Skin[0].sprite = Ob.Renai[1];
            Skin[1].sprite = Ob.Renai[2];
            Skin[2].sprite = Ob.Renai[0];
            Skin[3].sprite = Ob.Renai[0];
            Skin[4].sprite = Ob.Renai[0];
            Skin[5].sprite = Ob.Renai[0];
        }
    }

    void Update()
    {
        Armm();

        if (time)
        {
            rTime = rTime + 1 * Time.deltaTime; 
        }
        if(sc.Escape.activeSelf || sc.Enva.activeSelf || sc.Cons.activeSelf || sc.Die.activeSelf)
        {
            time = false;
        }
        else if (isGrounded && Ob.Mode == 0)
        {
            time = false;
            int a = Mathf.RoundToInt(rTime);
            if(rTime > 1)
            {
                sc.Men.hea = sc.Men.hea - a;
                sc.Hurt.Play();
            }
            rTime = 0;
        }
        else
        {
            time = true;
        }
        

            // karakter yürüme, zıplama ve atlama    

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

        if (Input.GetMouseButton(0))
        {
            sc.Walk.SetActive(false);
            anim.Play("Hit");
            sc.Men.time = false;
        }
        else if(!isGrounded)
        {
            sc.Walk.SetActive(false);
            sc.Men.time = false;
        }
        else if (x != 0 || z != 0)
        {
            sc.Walk.SetActive(true);
            anim.Play("Walk");
        }
        else
        {
            sc.Walk.SetActive(false);
        }

        if(x != 0 || z != 0)
        {
            if(Ob.Mode == 0)
                sc.Men.time = true;
        }
        else
        {
            sc.Men.time = false;
        }

        if (!sc.Escape.activeSelf && !sc.Enva.activeSelf && !sc.Cons.activeSelf && !sc.Die.activeSelf && !sc.Com.activeSelf)
        {
            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(JumpHeight * -2 * gravity);
            }
            else if(Input.GetButtonDown("Jump") && Ob.Mode == 1)
            {
                velocity.y = Mathf.Sqrt(JumpHeight * -2 * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    void Armm()
    {

        if (Ob.infos.TypeID == 0)
        {
            Hand.SetActive(true);
        }
        else
        {
            Hand.SetActive(false);
        }

        if (Ob.infos.TypeID == 3)
        {
            ArmOb.SetActive(true);
            ArmSp.sprite = Ob.infos.sprite;
        }
        else
        {
            ArmOb.SetActive(false);
        }

        if (Ob.infos.TypeID == 4 || Ob.infos.TypeID == 1 || Ob.infos.TypeID == 2)
        {
            TooOb.SetActive(true);
            TooSp.sprite = Ob.infos.sprite;
        }
        else
        {
            TooOb.SetActive(false);
        }
    }
}
