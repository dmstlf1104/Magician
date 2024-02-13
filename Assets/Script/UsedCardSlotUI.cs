using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsedCardSoltUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    private UsedCardSlot curSlot;

    public int index;
    public bool used;


    public void Set(UsedCardSlot slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.card.icon;

    }

    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
    }

    public void OnButtonClick()
    {
        UnitManagement.instance.SelectItem(index);
    }
}
