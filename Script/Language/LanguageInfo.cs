using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lan", menuName = "LanI")]
public class LanguageInfo : ScriptableObject
{
    public string isim;
    public string[] Texts, Parts;
    public string[] Games, Create;
    public int Language_Number;
}
