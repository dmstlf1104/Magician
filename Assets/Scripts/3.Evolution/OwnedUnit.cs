using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OwnedUnit : MonoBehaviour
{
    [SerializeField] private TMP_Text objectName;
    public Image thumbnail;
    [SerializeField] private Button CurrentCharBtn;    

    private Unit unit;
    private CurrentCharPanel currentCharPanel;
    private SelectEUnitList selectEUnitList;
    private void Awake()
    {
        currentCharPanel = FindObjectOfType<CurrentCharPanel>();
        selectEUnitList = FindObjectOfType<SelectEUnitList>();
    }

    public void InitOU(Unit unit)
    {
        this.unit = unit;

        objectName.text = "���� �⹰ �̸�";
        thumbnail.sprite = unit.Data.ObjectSprite;
        CurrentCharBtn.onClick.AddListener(() => currentCharPanel.CurrentCharDisplay(unit));        
        CurrentCharBtn.onClick.AddListener(() => selectEUnitList.InitEU(unit));        
    }
    
}