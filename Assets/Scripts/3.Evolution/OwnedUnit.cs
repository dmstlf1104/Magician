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

        objectName.text = "보유 기물 이름";
        thumbnail.sprite = unit.Data.ObjectSprite;
        CurrentCharBtn.onClick.AddListener(() => currentCharPanel.CurrentCharDisplay(unit));        
        CurrentCharBtn.onClick.AddListener(() => selectEUnitList.InitEUList(unit));
        ShopManager.Instance.EvolutionBtn.onClick.AddListener(() => Evolution(unit));
    }

    void Evolution(Unit unit)
    {
        this.unit = ShopManager.Instance.User.EvolutionUnits[unit.Data.UnitIndex].EUnits[unit.Data.UnitIndex];

        if (ShopManager.Instance.User.gold >= unit.Data.Price && unit.Data.EvolutionTrue == true)
        {
            ShopManager.Instance.User.gold -= unit.Data.Price;
            ShopManager.Instance.User.Inven.Remove(unit);
        }
        else
        {
            return;
        }
    }
}
