using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [Header("Info")]
    public string displayName;
    public string description;
    public Sprite icon;
    public bool used;
    public int atk;
    public int atkSpeed;

}