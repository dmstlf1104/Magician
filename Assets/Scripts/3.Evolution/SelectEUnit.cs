using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class SelectEUnit : MonoBehaviour
{
    [SerializeField] private TMP_Text objectName;
    [SerializeField] private Image thumbnail;
    [SerializeField] private Button detailExBtn;    
    [SerializeField] private TMP_Text detailExTxt;
    [SerializeField] private GameObject detailExPanel;
    [SerializeField] private Button selectBtn;

    private Unit unit;  

    bool isEtrue;    
    bool isPanelActive;    

    public void InitEU(Unit unit)
    {        
        this.unit = unit;
        
        objectName.text = unit.Data.Name;
        thumbnail.sprite = unit.Data.ObjectSprite;
        detailExBtn.onClick.AddListener(OndetailExPanel);
        selectBtn.onClick.AddListener(() => ShopManager.Instance.PriceValue(unit));
        selectBtn.onClick.AddListener(() => Etrue(unit));        
        ShopManager.Instance.EvolutionBtn.onClick.AddListener(() => Evolution(unit));
    }

    void OndetailExPanel()
    {
        isPanelActive = !isPanelActive;
        detailExPanel.SetActive(isPanelActive);
    }

    void Evolution(Unit unit)
    {
        this.unit = ShopManager.Instance.User.EvolutionUnits[unit.Data.UnitIndex].EUnits[unit.Data.UnitIndex];

        if (ShopManager.Instance.User.gold >= unit.Data.Price && unit.Data.EvolutionTrue == true)
        {
            ShopManager.Instance.User.gold -= unit.Data.Price;
            ShopManager.Instance.User.Inven.Add(unit);
        }
        else
        {
            return;
        }
    }

    void Etrue(Unit unit)
    {
        this.unit = unit;

        isEtrue = !isEtrue;
        if (isEtrue)
        {
            unit.Data.EvolutionTrue = true;
        }
        else if (!isEtrue)
        {
            unit.Data.EvolutionTrue = false;
        }
    }
}
