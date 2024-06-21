using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public SlotScript[] infos;
    public GameObject[] Slots;
    public BlocksData data;
    public SaveObject Object;
    public InterfaceScript sc;
    public int Craft;

    public int PageNm;
    public GameObject[] Pages;

    public GameObject Chest;
    public SlotScript[] Chest_Slot;
    public GameObject[] Chest_Obje;

    void Start()
    {
        
    }

    void Update()
    {
        if (!sc.Enva.activeSelf)
        {
            Sıfırla(2);
        }

        KeyK();

        for (int i = 0; i < infos.Length; i++)
        {
            if(infos[i].Choose)
            {
                if(infos[i].info != null)
                {
                    Object.Block = infos[i].info.Block;
                    Object.infos = infos[i].info;
                    Object.Script = infos[i];
                }
                else
                {
                    Object.Block = null;
                }
            }
        }
    }

    public SlotInfo Find(float id, float typeid)
    {
        for (int i = 0; i < data.info.Length; i++)
        {
            if (data.info[i].ID == id && data.info[i].TypeID == typeid)
            {
                return data.info[i];
            }
        }
        return null;
    }

    void KeyK()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i].Choose = false;
            }

            infos[0].Choose = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i].Choose = false;
            }

            infos[1].Choose = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i].Choose = false;
            }

            infos[2].Choose = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i].Choose = false;
            }

            infos[3].Choose = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i].Choose = false;
            }

            infos[4].Choose = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i].Choose = false;
            }

            infos[5].Choose = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i].Choose = false;
            }

            infos[6].Choose = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i].Choose = false;
            }

            infos[7].Choose = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i].Choose = false;
            }

            infos[8].Choose = true;
        }
    }

    public void ItemEkle(SlotInfo info,int Amout)
    {
        for (int i = 0; i < infos.Length; i++)
        {
            if(infos[i].info.ID == 0)
            {
                infos[i].info = info;
                infos[i].Amount = Amout;
                break;
            }
            if(infos[i].info == info)
            {
                if(infos[i].info.TypeID == 3)
                {
                    if(infos[i].info.ID > 15)
                    {
                        infos[i].Amount = infos[i].Amount + Amout;
                        if (infos[i].Amount > 64)
                        {
                            Amout = infos[i].Amount - 64;
                            infos[i].Amount = 64;
                            continue;
                        }
                        else if (infos[i].Amount == 64)
                        {
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    infos[i].Amount = infos[i].Amount + Amout;
                    if (infos[i].Amount > 64)
                    {
                        Amout = infos[i].Amount - 64;
                        infos[i].Amount = 64;
                        continue;
                    }
                    else if (infos[i].Amount == 64)
                    {
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
            }
        }
    }

    public void ThisArrayItemEkle(SlotInfo info, int Amout, SlotScript[] Slots)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].info.ID == 0)
            {
                Slots[i].info = info;
                Slots[i].Amount = Amout;
                break;
            }
            if (Slots[i].info == info)
            {
                if (Slots[i].info.TypeID == 3)
                {
                    if (Slots[i].info.ID > 15)
                    {
                        Slots[i].Amount = Slots[i].Amount + Amout;
                        if (Slots[i].Amount > 64)
                        {
                            Amout = Slots[i].Amount - 64;
                            Slots[i].Amount = 64;
                            continue;
                        }
                        else if (Slots[i].Amount == 64)
                        {
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Slots[i].Amount = Slots[i].Amount + Amout;
                    if (Slots[i].Amount > 64)
                    {
                        Amout = Slots[i].Amount - 64;
                        Slots[i].Amount = 64;
                        continue;
                    }
                    else if (Slots[i].Amount == 64)
                    {
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

            }
        }
    }

    public void sıfırla(SlotScript slot)
    {
        slot.info = data.info[28];
        slot.Amount = 0;
    }

    public void yükle(SlotScript Sl1, SlotScript Sl2)
    {
        Sl1.info = Sl2.info;
        Sl1.Amount = Sl2.Amount;
    }

    // 

    public void Sıfırla(float a)
    {
        if (a == 1)
        {
            for (int i = 0; i < 7; i++)
            {
                infos[i].Choose = false;
            }
        }
        if (a == 2)
        {
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i].ChooseIn = false;
            }
        }

    }

    public void Crafting(int cra)
    {
        Craft = cra;

        if(Craft == 1)
        {
            Craft_make(data.info[19], data.info[38], data.info[90], 1, data.info[27], 4);

        }

        if (Craft == 2)
        {
            Craft_make(data.info[27], 16, data.info[9], 1);

        }

        if (Craft == 3)
        {
            Craft_make(data.info[44], 9, data.info[16], 1);

        }

        if (Craft == 4)
        {
            Craft_make(data.info[27], 1, data.info[4], 4);

        }

        if (Craft == 5)
        {
            Craft_make_Multiple(data.info[4], 2, data.info[27], 3, data.info[31], 1);

        }

        if (Craft == 6)
        {
            Craft_make_Multiple(data.info[4], 2, data.info[40], 3, data.info[3], 1);

        }

        if (Craft == 7)
        {
            Craft_make_Multiple(data.info[4], 2, data.info[27], 3, data.info[29], 1);

        }

        if (Craft == 8)
        {
            Craft_make_Multiple(data.info[4], 2, data.info[27], 1, data.info[43], 1);

        }

        if (Craft == 9)
        {
            Craft_make_Multiple(data.info[4], 2, data.info[40], 3, data.info[2], 1);

        }

        if (Craft == 10)
        {
            Craft_make_Multiple(data.info[4], 2, data.info[40], 1, data.info[42], 1);

        }

        if (Craft == 11)
        {
            Craft_make_Multiple(data.info[4], 2, data.info[44], 3, data.info[34], 1);

        }

        if (Craft == 12)
        {
            Craft_make_Multiple(data.info[4], 2, data.info[44], 3, data.info[32], 1);

        }

        if (Craft == 13)
        {
            Craft_make_Multiple(data.info[4], 2, data.info[44], 1, data.info[41], 1);

        }

        if (Craft == 14)
        {
            Craft_make_Multiple(data.info[4], 2, data.info[44], 2, data.info[33], 1);

        }

        if (Craft == 15)
        {
            Craft_make_Multiple(data.info[4], 2, data.info[40], 2, data.info[36], 1);

        }

        if (Craft == 16)
        {
            Craft_make_Multiple(data.info[4], 2, data.info[27], 2, data.info[37], 1);

        }

        if (Craft == 17)
        {
            Craft_make(data.info[60], 3, data.info[58], 1);

        }

        if (Craft == 18)
        {
            Craft_make(data.info[44], 3, data.info[64], 1);

        }

        if (Craft == 19)
        {
            Craft_make(data.info[45], 9, data.info[83], 1);

        }

        if (Craft == 20)
        {
            Craft_make(data.info[48], 9, data.info[84], 1);

        }

        if (Craft == 21)
        {
            Craft_make(data.info[46], 9, data.info[85], 1);

        }

        if (Craft == 22)
        {
            Craft_make(data.info[27], 3, data.info[75], 1);

        }

        if (Craft == 23)
        {
            Craft_make(data.info[25], 3, data.info[74], 1);

        }

        if (Craft == 24)
        {
            Craft_make(data.info[40], 3, data.info[73], 1);

        }

        if (Craft == 25)
        {
            Craft_make(data.info[27], 3, data.info[67], 1);

        }

        if (Craft == 26)
        {
            Craft_make(data.info[25], 3, data.info[66], 1);

        }

        if (Craft == 27)
        {
            Craft_make(data.info[40], 3, data.info[65], 1);

        }

        if (Craft == 28)
        {
            Craft_make(data.info[27], 3, data.info[68], 1);

        }

        if (Craft == 29)
        {
            Craft_make(data.info[25], 3, data.info[69], 1);

        }

        if (Craft == 30)
        {
            Craft_make(data.info[40], 3, data.info[70], 1);

        }

        if (Craft == 31)
        {
            Craft_make(data.info[95], 1, data.info[99], 1);

        }

        if (Craft == 32)
        {
            Craft_make(data.info[91], 1, data.info[101], 1);

        }

        if (Craft == 33)
        {
            Craft_make(data.info[8], 1, data.info[100], 1);

        }

        if (Craft == 34)
        {
            Craft_make(data.info[15], 1, data.info[44], 1);

        }

        if (Craft == 35)
        {
            Craft_make(data.info[49], 1, data.info[48], 1);

        }

        if (Craft == 36)
        {
            Craft_make(data.info[11], 1, data.info[45], 1);

        }

        if (Craft == 37)
        {
            Craft_make(data.info[47], 1, data.info[46], 1);

        }

        if (Craft == 38)
        {
            Craft_make(data.info[40], 9, data.info[106], 1);

        }

        if (Craft == 39)
        {
            Craft_make(data.info[40], 9, data.info[102], 1);

        }

        if (Craft == 40)
        {
            Craft_make(data.info[22], 1, data.info[72], 1);

        }

        if (Craft == 41)
        {
            Craft_make(data.info[40], 1, data.info[25], 1);

        }

        if (Craft == 42)
        {
            Craft_make(data.info[108], 1, data.info[107], 1);

        }

        if (Craft == 43)
        {
            Craft_make(data.info[107], 9, data.info[109], 1);

        }

        if (Craft == 44)
        {
            Craft_make_Multiple(data.info[4], 1, data.info[27], 2, data.info[30], 1);

        }

        if (Craft == 45)
        {
            Craft_make_Multiple(data.info[4], 1, data.info[40], 2, data.info[5], 1);

        }

        if (Craft == 46)
        {
            Craft_make_Multiple(data.info[4], 1, data.info[44], 2, data.info[35], 1);

        }

    }

    public void Craft_make(SlotInfo need, int amount, SlotInfo give, int give_amount)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            SlotScript slot = Slots[i].GetComponent<SlotScript>();

            if (slot.info.ID == need.ID && slot.info.TypeID == need.TypeID)
            {
                if (slot.Amount >= amount)
                {
                    slot.Amount -= amount;
                    if(slot.Amount <= 0)
                    {
                        sıfırla(slot);
                    }

                    ItemEkle(give, give_amount);
                    sc.Craft.Play();
                    break;
                }
            }
        }
    }

    public void Craft_make(SlotInfo need,SlotInfo need2, int amount, SlotInfo give, int give_amount)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            SlotScript slot = Slots[i].GetComponent<SlotScript>();

            if ((slot.info.ID == need.ID || slot.info.ID == need2.ID) && slot.info.TypeID == need.TypeID)
            {
                if (slot.Amount >= amount)
                {
                    slot.Amount -= amount;
                    if (slot.Amount <= 0)
                    {
                        sıfırla(slot);
                    }

                    ItemEkle(give, give_amount);
                    sc.Craft.Play();
                    break;
                }
            }
        }
    }

    public void Craft_make(SlotInfo need, SlotInfo need2, SlotInfo need3, int amount, SlotInfo give, int give_amount)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            SlotScript slot = Slots[i].GetComponent<SlotScript>();

            if ((slot.info.ID == need.ID || slot.info.ID == need2.ID || slot.info.ID == need3.ID) && slot.info.TypeID == need.TypeID)
            {
                if (slot.Amount >= amount)
                {
                    slot.Amount -= amount;
                    if (slot.Amount <= 0)
                    {
                        sıfırla(slot);
                    }

                    ItemEkle(give, give_amount);
                    sc.Craft.Play();
                    break;
                }
            }
        }
    }

    public void Craft_make_Multiple(SlotInfo need, int amount, SlotInfo need2, int amount2, SlotInfo give, int give_amount)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            SlotScript slot = Slots[i].GetComponent<SlotScript>();

            for (int j = 0; j < Slots.Length; j++)
            {
                SlotScript slot2 = Slots[j].GetComponent<SlotScript>();

                if ((slot.info.ID == need.ID && slot.info.TypeID == need.TypeID) && (slot2.info.ID == need2.ID && slot2.info.TypeID == need2.TypeID))
                {
                    if (slot.Amount >= amount && slot2.Amount >= amount2)
                    {
                        slot.Amount -= amount;
                        slot2.Amount -= amount2;
                        if (slot.Amount <= 0)
                        {
                            sıfırla(slot);
                        }
                        if (slot2.Amount <= 0)
                        {
                            sıfırla(slot2);
                        }

                        ItemEkle(give, give_amount);
                        sc.Craft.Play();
                        break;
                    }
                }
            }
        }
    }

    // Pages

    public void Page_Change(int a)
    {
        PageNm += a;
        if(PageNm < 0)
        {
            PageNm = Pages.Length - 1;
        }
        if(PageNm > Pages.Length - 1)
        {
            PageNm = 0;
        }
        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i].SetActive(false);
        }
        Pages[PageNm].SetActive(true);

    }

    // Chest

    public void Chest_Load()
    {
        for (int i = 0; i < 27; i++)
        {
            Chest_Slot[i].info = Object.chest.Chest_Slot[i].info;
            Chest_Slot[i].Amount = Object.chest.Chest_Slot[i].Amount;
        }
    }

    public void Chest_Download()
    {
        for (int i = 0; i < 27; i++)
        {
            Object.chest.Chest_Slot[i].info = Chest_Slot[i].info;
            Object.chest.Chest_Slot[i].Amount = Chest_Slot[i].Amount;
        }
    }
}
