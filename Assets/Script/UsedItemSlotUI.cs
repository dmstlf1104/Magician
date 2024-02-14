using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsedItemSoltUI : MonoBehaviour
{
    public Image icon;

    public int index;

    public void Set(ItemSlot slot)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;
    }

    public void Clear()
    {
        icon.gameObject.SetActive(false);
        icon.sprite = null;
    }

    public void OnButtonClick()
    {
        UnitManagement.instance.SelectItem(index);
    }
}
