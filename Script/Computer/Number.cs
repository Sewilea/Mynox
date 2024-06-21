using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Number", menuName = "NumberI")]
public class Number : ScriptableObject
{
    public string numbername;
    public List<Vector2> number0;
    public List<Vector2> number1;
    public List<Vector2> number2;
    public List<Vector2> number3;
    public List<Vector2> number4;
    public List<Vector2> number5;
    public List<Vector2> number6;
    public List<Vector2> number7;
    public List<Vector2> number8;
    public List<Vector2> number9;

    [Header("Shape")]
    public List<Vector2> Block;
    public List<Vector2> Snowflake;
}
