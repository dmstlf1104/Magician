using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI quatityText;
    Unit unit;

    public int index;

    public void Set(Unit unit)
    {
        this.unit = unit;

        icon.gameObject.SetActive(true);
        icon.sprite = unit.Data.ObjectSprite;
        quatityText.text = unit.Data.Equip == true ? "»ç¿ëÁß" : string.Empty;
        index = unit.Data.UnitIndex;
    }

    public void Clear()
    {
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
    }

    public void OnButtonClick()
    {
        UnitManagement.instance.SelectItem(index);
    }
}
