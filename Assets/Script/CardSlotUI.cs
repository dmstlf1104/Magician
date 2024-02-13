using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSlotUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI useText;
    private CardData curCard;

    public int index;

    public void Set(CardData card)
    {
        curCard = card;
        icon.gameObject.SetActive(true);
        icon.sprite = card.icon;
        useText.text = card.used == true ? "»ç¿ëÁß" : string.Empty;

    }

    public void Clear()
    {
        curCard = null;
        icon.gameObject.SetActive(false);
        useText.text = string.Empty;
    }

    public void OnButtonClick()
    {
        UnitManagement.instance.SelectItem(index);
    }
}
