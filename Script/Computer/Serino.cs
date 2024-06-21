using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Serino : MonoBehaviour
{
    public GameObject Head, Pixel, Cursor;
    public Color White, Black;
    public int SizeX, SizeY;

    public GameObject Mouse;
    public int x, y;
    public Number number;

    public GameEngine Engine;


    string type;
    string property;
    public int Screen = 0;

    void Start()
    {
        Create_Panel();
        Start_Screen();

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            string Name = Mouse.name;
            One two = GameObject.Find(Name).GetComponent<One>();
            type = two.Type;
            property = two.property;

            string[] Commands = Mouse.name.Split(' ');

            x = int.Parse(Commands[0]);
            y = int.Parse(Commands[1]);

            if (two.property == "draw")
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    Shape_This(number.Block, true);
                }
                else
                {
                    Line_This(true);
                }
            }

            if (Screen == 2)
            {
                One three = GameObject.Find(Mouse.name).GetComponent<One>();

                int xo = x / 10;
                int xt = x - (xo * 10);

                int yo = y / 10;
                int yt = y - (yo * 10);

                Line(0, 17, 0, 6, false);
                Text(1, 1, xo);
                Text(5, 1, xt);
                Text(10, 1, yo);
                Text(14, 1, yt);
            }
        }

        if (Input.GetMouseButton(1))
        {
            string Name = Mouse.name;
            One two = GameObject.Find(Name).GetComponent<One>();
            type = two.Type;
            property = two.property;

            if (two.property == "draw")
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    Shape_This(number.Block, false);
                }
                else
                {
                    Line_This(false);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            string Name = Mouse.name;
            One two = GameObject.Find(Name).GetComponent<One>();
            type = two.Type;
            property = two.property;
            
            if(two.property == "quit")
            {
                Application.Quit();
            }

            if (two.property == "close")
            {
                Start_Screen();
                Screen = 0;
            }

            if (two.property == "paint")
            {
                Painter();
                Screen = 1;
            }

            if (two.property == "command")
            {
                Command();
                Screen = 3;
            }

            if (two.property == "text")
            {
                Number_Try();
                Screen = 2;

            }

            if (two.property == "first_test")
            {
                Engine.First_Test();
                Engine.Page = 1;

            }

            if (two.property == "gravity")
            {
                Engine.Gravity();
                Engine.Page = 2;

            }
        }

        // Interaction

        if(type == "button")
        {
            if(property == "number_text")
            {
                if (Input.GetKeyDown(KeyCode.Alpha0))
                {
                    Line(23,26, 7, 13, false);
                    Text(23, 7, 0);
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Line(23, 26, 7, 13, false);
                    Text(23, 7, 1);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    Line(23, 26, 7, 13, false);
                    Text(23, 7, 2);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    Line(23, 26, 7, 13, false);
                    Text(23, 7, 3);
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    Line(23, 26, 7, 13, false);
                    Text(23, 7, 4);
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    Line(23, 26, 7, 13, false);
                    Text(23, 7, 5);
                }
                if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    Line(23, 26, 7, 13, false);
                    Text(23, 7, 6);
                }
                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    Line(23, 26, 7, 13, false);
                    Text(23, 7, 7);
                }
                if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    Line(23, 26, 7, 13, false);
                    Text(23, 7, 8);
                }
                if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    Line(23, 26, 7, 13, false);
                    Text(23, 7, 9);
                }
            }
        } 
    }

    public void Create_Panel()
    {
        for (int X = 0; X < SizeX; X++)
        {
            for (int Y = 0; Y < SizeY; Y++)
            {
                GameObject Obje = Instantiate(Pixel, Head.transform);
                string StX = X.ToString();
                string StY = Y.ToString();

                if (X < 10)
                {
                    StX = 0 + X.ToString();
                }
                if (Y < 10)
                {
                    StY = 0 + Y.ToString();
                }

                Obje.name = StX + " " + StY;
            }
        }
    }

    // Screen

    public void Start_Screen()
    {
        Line(0, SizeX, 0, SizeY, false);
        Button(1, 4, 6, 9, true, "paint");
        Button(1, 4, 10, 13, true, "text");
        Button(1, 4, 14, 17, true, "command");
        Line(0, SizeX, 4, 5, true);
        //Button(1, 3, 1, 3, true, "quit");
        Shape(20, 12, number.Snowflake);
    }
    public void Painter()
    {
        Line(0, SizeX, 0, SizeY, false);
        Button(1, 3, 22, 24, true, "close");
        Line(0, SizeX, 20, 21, true);
        Draw(0, SizeX, 0, 20);
    }

    public void Command()
    {
        Line(0, SizeX, 0, SizeY, false);
        Button(1, 3, 22, 24, true, "close");
        Line(0, SizeX, 20, 21, true);
        Button(1, 4, 1, 4, true, "first_test");
        Button(5, 8, 1, 4, true, "gravity");
        //Player(14, 5);
    }

    public void Number_Try()
    {
        Line(0, SizeX, 0, SizeY, false);
        Button(1, 3, 22, 24, true, "close");
        Line(0, SizeX, 20, 21, true);
        Text(1, 13, 1);
        Text(5, 13, 2);
        Text(9, 13, 3);
        Text(13, 13, 4);
        Text(17, 13, 5);
        Text(1, 7, 6);
        Text(5, 7, 7);
        Text(9, 7, 8);
        Text(13, 7, 9);
        Text(17, 7, 0);
        Button(23, 26, 14, 17, true, "number_text");
    }

    // Text

    public void Text(int x, int y, int numbers)
    {
        if(numbers == 0)
        {
            for (int i = 0; i < number.number0.Count; i++)
            {
                int xx = x + (int)number.number0[i].x;
                int yy = y + (int)number.number0[i].y;
                Line(xx, yy, true);
            }
        }
        if (numbers == 1)
        {
            for (int i = 0; i < number.number1.Count; i++)
            {
                int xx = x + (int)number.number1[i].x;
                int yy = y + (int)number.number1[i].y;
                Line(xx, yy, true);
            }
        }
        if (numbers == 2)
        {
            for (int i = 0; i < number.number2.Count; i++)
            {
                int xx = x + (int)number.number2[i].x;
                int yy = y + (int)number.number2[i].y;
                Line(xx, yy, true);
            }
        }
        if (numbers == 3)
        {
            for (int i = 0; i < number.number3.Count; i++)
            {
                int xx = x + (int)number.number3[i].x;
                int yy = y + (int)number.number3[i].y;
                Line(xx, yy, true);
            }
        }
        if (numbers == 4)
        {
            for (int i = 0; i < number.number4.Count; i++)
            {
                int xx = x + (int)number.number4[i].x;
                int yy = y + (int)number.number4[i].y;
                Line(xx, yy, true);
            }
        }
        if (numbers == 5)
        {
            for (int i = 0; i < number.number5.Count; i++)
            {
                int xx = x + (int)number.number5[i].x;
                int yy = y + (int)number.number5[i].y;
                Line(xx, yy, true);
            }
        }
        if (numbers == 6)
        {
            for (int i = 0; i < number.number6.Count; i++)
            {
                int xx = x + (int)number.number6[i].x;
                int yy = y + (int)number.number6[i].y;
                Line(xx, yy, true);
            }
        }
        if (numbers == 7)
        {
            for (int i = 0; i < number.number7.Count; i++)
            {
                int xx = x + (int)number.number7[i].x;
                int yy = y + (int)number.number7[i].y;
                Line(xx, yy, true);
            }
        }
        if (numbers == 8)
        {
            for (int i = 0; i < number.number8.Count; i++)
            {
                int xx = x + (int)number.number8[i].x;
                int yy = y + (int)number.number8[i].y;
                Line(xx, yy, true);
            }
        }
        if (numbers == 9)
        {
            for (int i = 0; i < number.number9.Count; i++)
            {
                int xx = x + (int)number.number9[i].x;
                int yy = y + (int)number.number9[i].y;
                Line(xx, yy, true);
            }
        }
    }

    public void Shape(int x, int y, List<Vector2> Vector)
    {
        for (int i = 0; i < Vector.Count; i++)
        {
            int xx = x + (int)Vector[i].x;
            int yy = y + (int)Vector[i].y;
            Line(xx, yy, true);
        }
    }

    // Game Engine

    public void Player(int x,int y)
    {
        for (int i = 0; i < number.Block.Count; i++)
        {
            int xx = x + (int)number.Block[i].x;
            int yy = y + (int)number.Block[i].y;
            Point(xx, yy, true,"line","player");
        }
    }

    // Line

    public void Line(int x, int y,bool black)
    {
        string StX = x.ToString();
        string StY = y.ToString();

        if (x < 10)
        {
            StX = 0 + x.ToString();
        }
        if (y < 10)
        {
            StY = 0 + y.ToString();
        }

        string total = StX + " " + StY;
        if (black)
        {
            GameObject.Find(total).GetComponent<Image>().color = Black;
            One Image = GameObject.Find(total).GetComponent<One>();
            GameObject.Find(total).GetComponent<One>().Type = "line";
        }
        else
        {
            GameObject.Find(total).GetComponent<Image>().color = White;
            GameObject.Find(total).GetComponent<One>().Type = "";
            GameObject.Find(total).GetComponent<One>().property = "";
        }
    }

    public void Point(int x, int y, bool black, string type, string property)
    {
        string StX = x.ToString();
        string StY = y.ToString();

        if (x < 10)
        {
            StX = 0 + x.ToString();
        }
        if (y < 10)
        {
            StY = 0 + y.ToString();
        }

        string total = StX + " " + StY;
        if (black)
        {
            GameObject.Find(total).GetComponent<Image>().color = Black;
            One Image = GameObject.Find(total).GetComponent<One>();
            GameObject.Find(total).GetComponent<One>().Type = type;
            GameObject.Find(total).GetComponent<One>().property = property;
        }
        else
        {
            GameObject.Find(total).GetComponent<Image>().color = White;
            GameObject.Find(total).GetComponent<One>().Type = "";
            GameObject.Find(total).GetComponent<One>().property = "";
        }
    }

    public void Shape_This(List<Vector2> Vector, bool black)
    {
        for (int i = 0; i < Vector.Count; i++)
        {
            int xx = x + (int)Vector[i].x;
            int yy = y + (int)Vector[i].y;
            Line(xx, yy, black);
        }
    }

    public void Line_This(bool black)
    {
        if (black)
        {
            Mouse.GetComponent<Image>().color = Black;
            Mouse.GetComponent<One>().Type = "line";
        }
        else
        {
            Mouse.GetComponent<Image>().color = White;
            Mouse.GetComponent<One>().Type = "";
        }
    }

    public void Line(int xs,int xe, int ys, int ye, bool black)
    {
        for (int i = xs; i < xe; i++)
        {
            for (int j = ys; j < ye; j++)
            {
                string StX = i.ToString();
                string StY = j.ToString();

                if (i < 10)
                {
                    StX = 0 + i.ToString();
                }
                if (j < 10)
                {
                    StY = 0 + j.ToString();
                }

                string total = StX + " " + StY;
                if (black)
                {
                    GameObject.Find(total).GetComponent<Image>().color = Black;
                    GameObject.Find(total).GetComponent<One>().Type = "line";
                }
                else
                {
                    GameObject.Find(total).GetComponent<Image>().color = White;
                    GameObject.Find(total).GetComponent<One>().property = "";
                    GameObject.Find(total).GetComponent<One>().Type = "";
                }
            }
        }
    }

    // Button

    public void Button(int x, int y, bool black,string property)
    {
        string StX = x.ToString();
        string StY = y.ToString();

        if (x < 10)
        {
            StX = 0 + x.ToString();
        }
        if (y < 10)
        {
            StY = 0 + y.ToString();
        }

        string total = StX + " " + StY;
        if (black)
        {
            GameObject.Find(total).GetComponent<Image>().color = Black;
            One Image = GameObject.Find(total).GetComponent<One>();
            Image.property = property;
            GameObject.Find(total).GetComponent<One>().Type = "button";
        }
        else
        {
            GameObject.Find(total).GetComponent<Image>().color = White;
            GameObject.Find(total).GetComponent<One>().Type = "";
        }
    }

    public void Button(int xs, int xe, int ys, int ye, bool black , string property)
    {
        for (int i = xs; i < xe; i++)
        {
            for (int j = ys; j < ye; j++)
            {
                string StX = i.ToString();
                string StY = j.ToString();

                if (i < 10)
                {
                    StX = 0 + i.ToString();
                }
                if (j < 10)
                {
                    StY = 0 + j.ToString();
                }

                string total = StX + " " + StY;
                if (black)
                {
                    GameObject.Find(total).GetComponent<Image>().color = Black;
                    One Image = GameObject.Find(total).GetComponent<One>();
                    Image.property = property;
                    GameObject.Find(total).GetComponent<One>().Type = "button";
                }
                else
                {
                    GameObject.Find(total).GetComponent<Image>().color = White;
                    GameObject.Find(total).GetComponent<One>().Type = "";
                }
            }
        }
    }

    // Draw

    public void Draw(int x, int y)
    {
        string StX = x.ToString();
        string StY = y.ToString();

        if (x < 10)
        {
            StX = 0 + x.ToString();
        }
        if (y < 10)
        {
            StY = 0 + y.ToString();
        }

        string total = StX + " " + StY;
        GameObject.Find(total).GetComponent<Image>().color = White;
        GameObject.Find(total).GetComponent<One>().property = "draw";
    }

    public void Draw(int xs, int xe, int ys, int ye)
    {
        for (int i = xs; i < xe; i++)
        {
            for (int j = ys; j < ye; j++)
            {
                string StX = i.ToString();
                string StY = j.ToString();

                if (i < 10)
                {
                    StX = 0 + i.ToString();
                }
                if (j < 10)
                {
                    StY = 0 + j.ToString();
                }

                string total = StX + " " + StY;
                GameObject.Find(total).GetComponent<Image>().color = White;
                GameObject.Find(total).GetComponent<One>().property = "draw";
            }
        }
    }
}
