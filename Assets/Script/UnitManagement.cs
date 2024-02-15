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


public class ItemSlot
{
    public ItemData item;
}

public class UnitManagement : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;
    public UsedItemSoltUI[] usedUISlots;
    public ItemSlot[] usedSlots;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemAtk;
    public TextMeshProUGUI selectedItemAtkSpeed;
    public GameObject useButton;
    public GameObject unUseButton;
    public GameObject dropButton;
    public GameObject backButton;

    public ItemObject FireBall;

    public GameObject BG;

    public static UnitManagement instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SceneManager.LoadScene("IntroScene");
        slots = new ItemSlot[uiSlots.Length];
        usedSlots = new ItemSlot[usedUISlots.Length];
        
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }
        for (int i = 0; i < usedSlots.Length; i++)
        {
            usedSlots[i] = new ItemSlot();
            usedUISlots[i].index = i;
            usedUISlots[i].Clear();
        }
        FireBall.OnAddItem();
        ClearSelectedItemWindow();
        BG.SetActive(false);
    }

    public void AddItem(ItemData item)
    {
        ItemSlot emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.item = item;
            UpdateUI();
            return;
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
                uiSlots[i].Set(slots[i]);

            else
                uiSlots[i].Clear();
        }
        for (int i = 0; i < usedUISlots.Length; i++)
        {
            if (usedSlots[i].item != null)
                usedUISlots[i].Set(usedSlots[i]);
            else
                usedUISlots[i].Clear();
        }
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }
        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null)
            return;
        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;
        selectedItemAtk.text = "공격력:" + selectedItem.item.atk.ToString();
        selectedItemAtkSpeed.text = "공격속도" + selectedItem.item.atkSpeed.ToString();
        useButton.SetActive(selectedItem.item.used == false);
        unUseButton.SetActive(selectedItem.item.used == true);
        dropButton.SetActive(true);
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
    }

    public void OnUseButton()
    {
        slots[selectedItemIndex].item.used = true;
        for(int i=0;i<usedSlots.Length;i++)
        {
            if (usedSlots[i].item == null)
            {
                usedSlots[i].item = slots[selectedItemIndex].item;
                break;
            }
        }
        UpdateUI();

        SelectItem(selectedItemIndex);
    }

    void UnUse(int index)
    {
        slots[index].item.used = false;
        UpdateUI();
        if (selectedItemIndex == index)
        {
            SelectItem(index);
        }
    }

    public void OnUnUseButton()
    {
        slots[selectedItemIndex].item.used = false;
        for (int i = 0; i < usedSlots.Length; i++)
        {
            if (usedSlots[i].item != null && usedSlots[i].item.used == false)
            {
                usedSlots[i].item = null;
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

    public void OnBackButton()
    {
        BG.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }

    public void OnStart()
    {
        BG.SetActive(true);
        SceneManager.LoadScene("UnitManagementScene");
    }

    private void RemoveSelectedItem()
    {
        slots[selectedItemIndex].item.used = false;
        for (int i = 0; i < usedSlots.Length; i++)
        {
            if (usedSlots[i].item != null && usedSlots[i].item.used == false)
            {
                usedSlots[i].item = null;
                break;
            }
        }

        selectedItem.item = null;
        ClearSelectedItemWindow();
        UpdateUI();
    }

}
