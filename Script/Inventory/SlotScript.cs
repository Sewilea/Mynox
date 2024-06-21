using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour
{
    InterfaceScript sc;
    public SaveObject Ob;
    public Image ima, imaC, imaCI;
    public SlotInfo info;
    public int Amount;
    public bool Choose;
    public bool ChooseIn;
    public Inventory Inven;

    public Text Amount_Text;

    void Start()
    {
        sc = GameObject.Find("_Script").GetComponent<InterfaceScript>();
        Inven = GameObject.Find("_Script").GetComponent<Inventory>();
    }

    void Update()
    {
        imaC.enabled = Choose;
        imaCI.enabled = ChooseIn;

        if (info.ID != 0)
        {
            ima.enabled = true;
            ima.sprite = info.sprite;
        }
        else
        {
            ima.enabled = false;
        }

        if (Amount > 1)
        {
            Amount_Text.enabled = true;
            Amount_Text.text = Amount.ToString();
        }
        else
        {
            Amount_Text.enabled = false;
        }
    }

    public void OnPointer()
    {
        sc.Inv.Play();
        if(Ob.Mode == 1)
        {
            info = Inven.data.info[28];
            Amount = 0;
        }
        else
        {
            if (Inven.Chest.activeSelf)
            {
                ChooseIn = true;
                if (ChooseIn)
                {
                    for (int i = 0; i < Inven.Chest_Slot.Length; i++)
                    {
                        SlotScript slot = Inven.Chest_Slot[i];
                        if (slot.ChooseIn)
                        {
                            if (Inven.Chest_Obje[i] != gameObject)
                            {
                                slot.ChooseIn = false;
                                ChooseIn = false;

                                GameObject slot1 = Inven.Chest_Obje[i];
                                GameObject slot2 = gameObject;

                                SlotScript Sl1 = Inven.Chest_Slot[i];
                                SlotScript Sl2 = slot2.GetComponent<SlotScript>();

                                if (Sl1.info.TypeID == Sl2.info.TypeID && Sl1.info.ID == Sl2.info.ID)
                                {
                                    if (Sl1.info.TypeID == 3 && Sl1.info.ID > 15)
                                    {
                                        if (Sl1.Amount <= 64 && Sl2.Amount <= 64)
                                        {
                                            if (Sl1.Amount + Sl2.Amount <= 64)
                                            {
                                                Sl2.Amount += Sl1.Amount;
                                                Inven.sıfırla(Sl1);
                                            }
                                            if (Sl2.Amount + Sl1.Amount > 64)
                                            {
                                                int Slt1 = (Amount + Sl1.Amount) - 64;
                                                Amount += Sl1.Amount;
                                                Amount -= Slt1;
                                                Sl1.Amount = Slt1;
                                            }

                                            // 64 olunca biri değiş-tokuş sağlanmıyor
                                        }

                                    }
                                    else
                                    {
                                        if (Sl1.Amount <= 64 && Sl2.Amount <= 64)
                                        {
                                            if (Sl1.Amount + Sl2.Amount <= 64)
                                            {
                                                Sl2.Amount += Sl1.Amount;
                                                Inven.sıfırla(Sl1);
                                            }
                                            if (Sl2.Amount + Sl1.Amount > 64)
                                            {
                                                int Slt1 = (Amount + Sl1.Amount) - 64;
                                                Amount += Sl1.Amount;
                                                Amount -= Slt1;
                                                Sl1.Amount = Slt1;
                                            }

                                            // 64 olunca biri değiş-tokuş sağlanmıyor
                                        }
                                    }
                                }
                                else
                                {
                                    GameObject Slot1 = Instantiate(slot1, null);
                                    GameObject Slot2 = Instantiate(slot2, null);

                                    Inven.sıfırla(Sl2);
                                    Inven.sıfırla(Sl1);

                                    Inven.yükle(Sl2, Slot1.GetComponent<SlotScript>());
                                    Inven.yükle(Sl1, Slot2.GetComponent<SlotScript>());

                                    Destroy(Slot1);
                                    Destroy(Slot2);
                                }
                            }
                        }
                    }
                    Inven.Chest_Download();
                }
            }
            else
            {
                ChooseIn = true;
                if (ChooseIn)
                {
                    for (int i = 0; i < Inven.infos.Length; i++)
                    {
                        SlotScript slot = Inven.infos[i];
                        if (slot.ChooseIn)
                        {
                            if (Inven.Slots[i] != gameObject)
                            {
                                slot.ChooseIn = false;
                                ChooseIn = false;

                                GameObject slot1 = Inven.Slots[i];
                                GameObject slot2 = gameObject;

                                SlotScript Sl1 = Inven.infos[i];
                                SlotScript Sl2 = slot2.GetComponent<SlotScript>();

                                if (Sl1.info.TypeID == Sl2.info.TypeID && Sl1.info.ID == Sl2.info.ID)
                                {
                                    if (Sl1.info.TypeID == 3 && Sl1.info.ID > 15)
                                    {
                                        if (Sl1.Amount <= 64 && Sl2.Amount <= 64)
                                        {
                                            if (Sl1.Amount + Sl2.Amount <= 64)
                                            {
                                                Sl2.Amount += Sl1.Amount;
                                                Inven.sıfırla(Sl1);
                                            }
                                            if (Sl2.Amount + Sl1.Amount > 64)
                                            {
                                                int Slt1 = (Amount + Sl1.Amount) - 64;
                                                Amount += Sl1.Amount;
                                                Amount -= Slt1;
                                                Sl1.Amount = Slt1;
                                            }

                                            // 64 olunca biri değiş-tokuş sağlanmıyor
                                        }

                                    }
                                    else
                                    {
                                        if (Sl1.Amount <= 64 && Sl2.Amount <= 64)
                                        {
                                            if (Sl1.Amount + Sl2.Amount <= 64)
                                            {
                                                Sl2.Amount += Sl1.Amount;
                                                Inven.sıfırla(Sl1);
                                            }
                                            if (Sl2.Amount + Sl1.Amount > 64)
                                            {
                                                int Slt1 = (Amount + Sl1.Amount) - 64;
                                                Amount += Sl1.Amount;
                                                Amount -= Slt1;
                                                Sl1.Amount = Slt1;
                                            }

                                            // 64 olunca biri değiş-tokuş sağlanmıyor
                                        }
                                    }
                                }
                                else
                                {
                                    GameObject Slot1 = Instantiate(slot1, null);
                                    GameObject Slot2 = Instantiate(slot2, null);

                                    Inven.sıfırla(Sl2);
                                    Inven.sıfırla(Sl1);

                                    Inven.yükle(Sl2, Slot1.GetComponent<SlotScript>());
                                    Inven.yükle(Sl1, Slot2.GetComponent<SlotScript>());

                                    Destroy(Slot1);
                                    Destroy(Slot2);
                                }
                            }
                        }
                    }
                }
            }

            
        }
    }

    public void OnAdd()
    {
        Inven.ItemEkle(info,1);
        sc.Inv.Play();
    }

    public void Sıfırla(float a)
    {
        if (a == 1)
        {
            for (int i = 0; i < 7; i++)
            {
                Inven.infos[i].Choose = false;
            }
        }
        if (a == 2)
        {
            for (int i = 0; i < Inven.infos.Length; i++)
            {
                Inven.infos[i].ChooseIn = false;
            }
        }

    }
}
