using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "MapI")]
public class MapObject : ScriptableObject
{
    public string isim;
    public int value, Mode;
    public float CharX,CharY, CharZ;
    public List<MapChunk> Chunks;
}

[System.Serializable]
public class MapChunk
{
    public float x, y, z;
    public List<MapBlock> MapBlock;
    public MapChunk(float X, float Y, float Z,List<MapBlock> Block)
    {
        x = X;
        y = Y;
        z = Z;
        MapBlock = Block;
    }
}

[System.Serializable]
public class MapBlock
{
    public float x, y, z;
    public int ID, TypeID;

    public MapBlock(float X, float Y, float Z,int id,int typeid)
    {
        x = X;
        y = Y;
        z = Z;
        ID = id;
        TypeID = typeid;
    }
}
