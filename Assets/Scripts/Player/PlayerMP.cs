using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMP : MonoBehaviour
{
    [SerializeField] private int currentMana = 100;

    public int CurrentMana
    {
        set => currentMana = Mathf.Max(0, value);
        get => currentMana;
    }
}
