using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slot", menuName = "SlotI")]
public class SlotInfo : ScriptableObject
{
    public string slotName;
    public int ID, TypeID, UnderID;
    public Sprite sprite;
    public GameObject Block;

    [Header("Property")]
    public float Hit_time;
}
