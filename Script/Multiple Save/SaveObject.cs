using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "SaveI")]
public class SaveObject : ScriptableObject
{
    public int Number;
    public string isim, age;
    public int SeeAngular, Flats, Value, Map, Skin, SizeY, Mode, ChunkSize, CameraSize;
    public bool Flat, Normal;
    // Inventory
    public GameObject Block;
    public SlotScript Script;
    public SlotInfo infos;
    public LanguageInfo info;
    public Chest chest;
    // Raycast
    public SlotInfo RaycastBlock;
    public SlotInfo Null;
    // Rotation
    public string Message;
    // Skin
    public int Aspect, View;
    public Sprite[] Emo, CanvorQi, Minox, Serino, Renai;
    // Choose Broke
    public float Break;
    public MapObject Maps;
    public int SizeC;
}
