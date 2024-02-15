using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class SellUnit : MonoBehaviour
{
    [SerializeField] private TMP_Text objectName;
    [SerializeField] private Image thumbnail;
    [SerializeField] private Button detailExBtn;
    [SerializeField] private GameObject detailExPanel;
    [SerializeField] private TMP_Text detailExTxt;
    [SerializeField] private TMP_Text objectTxt;
    [SerializeField] private Button buyBtn;
    [SerializeField] private GameObject buyCompletePanel;    

    bool isPanelActive = false;

    private Unit unit;
    private ShopManager shopManager;
    private UnitManagement unitManagement;    
    private Inventory inventory;
    private ItemObject itemObject;

    private void Awake()
    {
        shopManager = ShopManager.Instance;
        unitManagement = FindObjectOfType<UnitManagement>();
        inventory = FindObjectOfType<Inventory>();
        itemObject = FindObjectOfType<ItemObject>();
    }

    public void InitU(Unit unit)
    {
        this.unit = unit;

        objectName.text = unit.Data.Name;
        thumbnail.sprite = unit.Data.ObjectSprite;
        detailExTxt.text = unit.Data.detailEx;
        objectTxt.text = $"공격력 : {unit.Data.Stat.atk.ToString()}     치명타 : {unit.Data.Stat.crt.ToString()}\n공격속도 : {unit.Data.Stat.atkS.ToString()}\n가격 : {unit.Data.Price.ToString()} Gold";
        detailExBtn.onClick.AddListener(OndetailExPanel);
        buyBtn.onClick.AddListener(() => OnBuyCompletePanel(unit));
    }

    void OndetailExPanel()
    {
        isPanelActive = !isPanelActive;
        detailExPanel.SetActive(isPanelActive);
    }

    void OnBuyCompletePanel(Unit unit)
    {
        this.unit = unit;

        if(shopManager.User.gold >= unit.Data.Price)
        {
            shopManager.User.gold -= unit.Data.Price;
            buyCompletePanel.SetActive(true);
            shopManager.PlayerGold.text = shopManager.User.gold.ToString();
            shopManager.User.Inven.Add(unit);                    
        }
        else
        {
            shopManager.UnitNotMoney.SetActive(true);
        }
        
    }
}
