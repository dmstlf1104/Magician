using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    Unit item;
    public void OnAddItem()
    {
        UnitManagement.instance.AddItem(item);
    }
}
