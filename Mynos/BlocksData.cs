using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "BlockData")]
public class BlocksData : ScriptableObject
{
    public GameObject[] Blocks, Grass, Water, Grow;
    public SlotInfo[] info;
    public Sprite[] Block;
}
