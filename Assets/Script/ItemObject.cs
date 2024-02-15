using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public UnitData item;

    public void OnAddItem()
    {
        UnitManagement.instance.AddItem(item);
    }
}
