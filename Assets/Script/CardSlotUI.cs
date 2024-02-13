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
    private CardSlot curSlot;
    private Outline outline;

    public int index;
    public bool used;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = used;
    }
    public void Set(CardSlot slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.card.icon;
        useText.text = slot.card.used == true ? "»ç¿ëÁß" : string.Empty;

        if (outline != null)
        {
            outline.enabled = used;
        }

    }

    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
        useText.text = string.Empty;
    }

    public void OnButtonClick()
    {
        UnitManagement.instance.SelectItem(index);
    }
}
