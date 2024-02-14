using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RelicData_", menuName = "Data/Relic")]
public class RelicData : ScriptableObject
{
    public string Name;
    public string detailEx;
    public Sprite ObjectSprite;
    public Status Stat;
    public int Price;
    public bool FireType;
    public bool FrostType;
    public bool EarthType;
}
