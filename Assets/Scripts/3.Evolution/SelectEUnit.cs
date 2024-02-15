using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectEUnit : MonoBehaviour
{
    [SerializeField] private TMP_Text objectName;
    [SerializeField] private Image thumbnail;
    [SerializeField] private Button detailExBtn;    
    [SerializeField] private TMP_Text detailExTxt;
    [SerializeField] private GameObject detailExPanel;
    [SerializeField] private Button selectBtn;    

    private SelectEvolutionUnit selectEvolutionUnit;
    Unit unit;
    

    bool isPanelActive;

    private void Awake()
    {
        ShopManager.Instance = FindObjectOfType<ShopManager>();
    }

    public void InitEU(SelectEvolutionUnit selectEvolutionUnit)
    {
        this.selectEvolutionUnit = selectEvolutionUnit;

        objectName.text = selectEvolutionUnit.Data.Name;
        thumbnail.sprite = selectEvolutionUnit.Data.ObjectSprite;
        detailExBtn.onClick.AddListener(OndetailExPanel);
        selectBtn.onClick.AddListener(() => ShopManager.Instance.PriceValue(selectEvolutionUnit));
        //ShopManager.Instance.EvolutionBtn.onClick.AddListener();
    }

    void OndetailExPanel()
    {
        isPanelActive = !isPanelActive;
        detailExPanel.SetActive(isPanelActive);
    }  
    
    void Evolution(Unit unit, SelectEvolutionUnit selectEvolutionUnit)
    {
        this.unit = unit;
        this.selectEvolutionUnit = selectEvolutionUnit;

        
    }
}
