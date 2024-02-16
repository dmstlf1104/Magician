using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static UnityEditor.Progress;


//public class Unit
//{
//    public UnitData item;    
//}

public class UnitManagement : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public Unit[] slots;
    public UsedItemSoltUI[] usedUISlots;
    public Unit[] usedSlots;

    [Header("Selected Item")]
    private Unit selectedItem;
    private int selectedItemIndex;
    public Scene storeScene;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemAtk;
    public TextMeshProUGUI selectedItemAtkSpeed;
    public GameObject useButton;
    public GameObject unUseButton;
    public GameObject dropButton;
    public GameObject backButton;

    public GameObject upgradeButton;
    public GameObject speedUpgradeButton;
    public GameObject atkUpgradeButton;
    public GameObject cancelButton;

    public ItemObject FireBall;

    public GameObject BG;

    public static UnitManagement instance;
    Inventory inventory;
    ShopManager shopManager;

    void Awake()
    {
        instance = this;
        shopManager = FindObjectOfType<ShopManager>();
        slots = shopManager.User.Inven.ToArray();
        int i = 0;
        foreach (Unit unit in shopManager.User.Inven)
        {
            slots[i] = unit;
            i++;
        }
        UpdateUI();
    }

    private void Start()
    {
        //slots = new Unit[uiSlots.Length];
        usedSlots = new Unit[usedUISlots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            //slots[i] = new Unit();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }
        for (int i = 0; i < usedSlots.Length; i++)
        {
            usedSlots[i] = new Unit();
            usedUISlots[i].index = i;
            usedUISlots[i].Clear();
        }
        //FireBall.OnAddItem();
        ClearSelectedItemWindow();
    }

    public void AddItem(Unit item)
    {
        Unit emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot = item;
            UpdateUI();
            return;
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Data != null)
                uiSlots[i].Set(slots[i]);

            else
                uiSlots[i].Clear();
        }
        for (int i = 0; i < usedUISlots.Length; i++)
        {
            if (usedSlots[i].Data != null)
                usedUISlots[i].Set(usedSlots[i]);
            else
                usedUISlots[i].Clear();
        }
    }

    Unit GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Data == null)
                return slots[i];
        }
        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].Data == null)
            return;
        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.Data.Name;
        selectedItemDescription.text = selectedItem.Data.detailEx;
        selectedItemAtk.text = "공격력:" + selectedItem.Data.Stat.atk.ToString();
        selectedItemAtkSpeed.text = "공격속도" + selectedItem.Data.Stat.atkS.ToString();
        useButton.SetActive(selectedItem.Data.Equip == false);
        unUseButton.SetActive(selectedItem.Data.Equip == true);
        dropButton.SetActive(true);
        upgradeButton.SetActive(true);
    }

    private void ClearSelectedItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemAtk.text = string.Empty;
        selectedItemAtkSpeed.text = string.Empty;
        useButton.SetActive(false);
        unUseButton.SetActive(false);
        dropButton.SetActive(false);
        upgradeButton.SetActive(false);
    }

    public void OnUseButton()
    {
        slots[selectedItemIndex].Data.Equip = true;
        for (int i = 0; i < usedSlots.Length; i++)
        {
            if (usedSlots[i].Data == null)
            {
                usedSlots[i].Data = slots[selectedItemIndex].Data;
                break;
            }
        }
        UpdateUI();

        SelectItem(selectedItemIndex);
    }

    void UnUse(int index)
    {
        slots[index].Data.Equip = false;
        UpdateUI();
        if (selectedItemIndex == index)
        {
            SelectItem(index);
        }
    }

    public void OnUnUseButton()
    {
        slots[selectedItemIndex].Data.Equip = false;
        for (int i = 0; i < usedSlots.Length; i++)
        {
            if (usedSlots[i].Data != null && usedSlots[i].Data.Equip == false)
            {
                usedSlots[i].Data = null;
                break;
            }
        }
        UpdateUI();

        SelectItem(selectedItemIndex);
    }

    public void OnDropButton()
    {
        RemoveSelectedItem();
    }

    public void OnUpgradeButton()
    {
        upgradeButton.SetActive(false);
        speedUpgradeButton.SetActive(true);
        atkUpgradeButton.SetActive(true);
        cancelButton.SetActive(true);
    }

    public void OnCancelButton()
    {
        upgradeButton.SetActive(true);
        speedUpgradeButton.SetActive(false);
        atkUpgradeButton.SetActive(false);
        cancelButton.SetActive(false);
    }

    public void OnSpeedUpgradeButton()
    {
        upgradeButton.SetActive(true);
        speedUpgradeButton.SetActive(false);
        atkUpgradeButton.SetActive(false);
        cancelButton.SetActive(false);
        slots[selectedItemIndex].Data.Stat.atkS += 1;
        SelectItem(selectedItemIndex);
    }

    public void OnAtkUpgradeButton()
    {
        upgradeButton.SetActive(true);
        speedUpgradeButton.SetActive(false);
        atkUpgradeButton.SetActive(false);
        cancelButton.SetActive(false);
        slots[selectedItemIndex].Data.Stat.atk += 5;
        SelectItem(selectedItemIndex);
    }

    public void OnBackButton()
    {
        BG.SetActive(false);
    }

    public void OnStart()
    {
        BG.SetActive(true);
    }

    private void RemoveSelectedItem()
    {
        slots[selectedItemIndex].Data.Equip = false;
        for (int i = 0; i < usedSlots.Length; i++)
        {
            if (usedSlots[i].Data != null && usedSlots[i].Data.Equip == false)
            {
                usedSlots[i].Data = null;
                break;
            }
        }

        selectedItem.Data = null;
        ClearSelectedItemWindow();
        UpdateUI();
    }

}
