using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData_", menuName = "Data/Unit")]
public class UnitData : ScriptableObject
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
