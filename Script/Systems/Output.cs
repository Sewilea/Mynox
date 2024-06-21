using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Output : MonoBehaviour
{
    public Chunk mychunk;
    public SpriteRenderer[] Sprites;
    public Sprite Open, Close;
    public bool Message;

    public bool times = false;
    public float timei;
    void Start()
    {
        mychunk = transform.parent.transform.parent.transform.GetComponent<Chunk>();
    }

    void Update()
    {
        if (times)
        {
            timei += 1 * Time.deltaTime;
        }
        if(timei > 0.2f)
        {
            timei = 0;
            MessageChanged(false);
        }

        if (Message && timei > 0.1f)
        {
            for (int j = 0; j < mychunk.Behind.Count; j++)
            {
                GameObject Chunks = mychunk.Behind[j].Object;
                for (int i = 0; i < Chunks.GetComponent<Chunk>().Block.Count; i++)
                {
                    GameObject BlockV = Chunks.GetComponent<Chunk>().Block[i];
                    Vector3 Block_Vektor = BlockV.transform.position;
                    Vector3 My_Vector = gameObject.transform.position;
                    BlockInfo info = Chunks.GetComponent<Chunk>().Block[i].GetComponent<BlockInfo>();

                    bool done = false;

                    if (info.slot.ID == 46)
                    {
                        if (Block_Vektor.x + 1 == My_Vector.x && Block_Vektor.y == My_Vector.y && Block_Vektor.z == My_Vector.z)
                        {
                            done = true;
                        }

                        if (Block_Vektor.x - 1 == My_Vector.x && Block_Vektor.y == My_Vector.y && Block_Vektor.z == My_Vector.z)
                        {
                            done = true;
                        }

                        if (Block_Vektor.x == My_Vector.x && Block_Vektor.y == My_Vector.y && Block_Vektor.z - 1 == My_Vector.z)
                        {
                            done = true;
                        }

                        if (Block_Vektor.x == My_Vector.x && Block_Vektor.y == My_Vector.y && Block_Vektor.z + 1 == My_Vector.z)
                        {
                            done = true;
                        }

                        if (Block_Vektor.x == My_Vector.x && Block_Vektor.y + 1 == My_Vector.y && Block_Vektor.z == My_Vector.z)
                        {
                            done = true;
                        }

                        if (Block_Vektor.x == My_Vector.x && Block_Vektor.y - 1 == My_Vector.y && Block_Vektor.z == My_Vector.z)
                        {
                            done = true;
                        }

                        if (done && !Chunks.GetComponent<Chunk>().Block[i].GetComponent<Output>().Message)
                        {
                            Chunks.GetComponent<Chunk>().Block[i].GetComponent<Output>().MessageChanged(true);
                            MessageChanged(false);
                        }
                    }
                }

            }
        }
    }

    public void MessageChanged(bool bol)
    {
        timei = 0;
        times = true;
        Message = bol;
        if (Message)
        {
            for (int i = 0; i < Sprites.Length; i++)
            {
                Sprites[i].sprite = Open;
            }
        }
        else
        {
            for (int i = 0; i < Sprites.Length; i++)
            {
                Sprites[i].sprite = Close;
            }
        }
    }
}
