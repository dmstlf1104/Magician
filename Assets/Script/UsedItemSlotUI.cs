using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsedItemSoltUI : MonoBehaviour
{
    public Image icon;

    public int index;

    public void Set(Unit slot)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = slot.Data.ObjectSprite;
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

    public void OnItemSet()
    {
        InGameItem.instance.OnItemSet(index);
    }
}
