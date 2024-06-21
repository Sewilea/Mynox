using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public Number number;
    public Serino Serino;
    public int px, py;
    public float speed, pyy, pxx;

    public int Page = 0;

    public float time;
    public bool timeb;

    void Start()
    {
        
    }

    void Update()
    {
        if (timeb)
        {
            time += 1 * Time.deltaTime;
        }
        else
        {
            time = 0;
        }

        if (Page <3)
        {
            if (Input.GetKey(KeyCode.W) && Page < 2)
            {
                Movement(0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Movement(1);
            }
            if (Input.GetKey(KeyCode.A))
            {
                Movement(2);
            }
            if (Input.GetKey(KeyCode.D))
            {
                Movement(3);
            }
        }

        if(Page == 2)
        {
            if(time > 0.2f)
            {
                time = 0;
                Movement(1);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                speed = 3;
                Movement(0);
                speed = 1;
            }
        }
    }

    public void First_Test()
    {
        Serino.Line(0, Serino.SizeX, 0, Serino.SizeY - 5, false);
        Serino.Button(1, 3, 22, 24, true, "command");
        Player(20, 5);
        px = 20; py = 5;
    }

    public void Gravity()
    {
        Serino.Line(0, Serino.SizeX, 0, Serino.SizeY - 5, false);
        Serino.Button(1, 3, 22, 24, true, "command");
        Player(20, 5);
        px = 20; py = 5;
    }

    public void Player(int x, int y)
    {
        for (int i = 0; i < number.Block.Count; i++)
        {
            int xx = x + (int)number.Block[i].x;
            int yy = y + (int)number.Block[i].y;
            Serino.Point(xx, yy, true, "line", "player");
        }
    }

    public void Movement(int Rotate)
    {
        if (Rotate == 0 && py < 18)
        {
            Serino.Line(px - 1, px + 2, py - 1, py + 2, false);
            pyy = py + 1 * speed;
            py = (int)pyy;
            Player(px, py);
        }
        if (Rotate == 1 && py > 1)
        {
            Serino.Line(px - 1, px + 2, py - 1, py + 2, false);
            pyy = py - 1 * speed;
            py = (int)pyy;
            Player(px, py);
        }
        if (Rotate == 2 && px > 1)
        {
            Serino.Line(px - 1, px + 2, py - 1, py + 2, false);
            pxx = px - 1 * speed;
            px = (int)pxx;
            Player(px, py);
        }
        if (Rotate == 3 && px < Serino.SizeX - 2)
        {
            Serino.Line(px - 1, px + 2, py - 1, py + 2, false);
            pxx = px + 1 * speed;
            px = (int)pxx;
            Player(px, py);
        }
    }
}
