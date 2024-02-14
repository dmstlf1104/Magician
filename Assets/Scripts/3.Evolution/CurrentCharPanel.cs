using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentCharPanel : MonoBehaviour
{
    [SerializeField] private Image CurrentCharImage;
    [SerializeField] private TMP_Text CurrentCharEx;
        
    private OwnedUnit ownedUnit;
    private Unit unit;

    public void CurrentCharDisplay(Unit unit)
    {
        this.unit = unit;

        CurrentCharImage.sprite = unit.Data.ObjectSprite;
        CurrentCharEx.text = "보유 기물 설명";        
    }
}
