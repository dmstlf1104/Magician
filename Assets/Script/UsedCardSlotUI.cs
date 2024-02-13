using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsedCardSoltUI : MonoBehaviour
{
    public Image icon;

    public void Set(CardData card)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = card.icon;
    }

    public void Clear()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
    }
}
