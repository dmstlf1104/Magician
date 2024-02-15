using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class InGameItem : MonoBehaviour
{
    UnitManagement unitManagement;

    public UsedItemSoltUI[] usedUISlots;
    public Unit[] usedSlots;

    public bool itemSet;

    public static InGameItem instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        unitManagement = UnitManagement.instance;
        usedSlots = new Unit[usedUISlots.Length];
        for (int i = 0; i < usedSlots.Length; i++)
        {
            usedSlots[i] = new Unit();
            usedUISlots[i].index = i;
            usedUISlots[i].Clear();
        }
        InGameUpdateUI();
    }

    public void InGameUpdateUI()
    {
        for (int i = 0; i < usedUISlots.Length; i++)
        {
            if (unitManagement.usedSlots[i] != null && unitManagement.usedUISlots[i]!= null)
            {
                usedSlots[i].Data = unitManagement.usedSlots[i].Data;
                if (usedSlots[i].Data != null)
                    usedUISlots[i].Set(unitManagement.usedSlots[i]);
                else
                    usedUISlots[i].Clear();
            }
        }
    }

    public void OnItemSet(int index)
    {
        if (usedSlots[index].Data == null)
            return;
        
    }

}
