using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static UnityEditor.Progress;


public class CardSlot
{
    public CardData card;
}

public class UsedCardSlot
{
    public CardData card;
}

public class UnitManagement : MonoBehaviour
{
    public CardSlotUI[] uiSlots;
    public static CardSlot[] slots = new CardSlot[18];

    public UsedCardSoltUI[] usedUISlots;
    public static UsedCardSlot[] usedSlots = new UsedCardSlot[5];

    [Header("Selected Item")]
    private CardSlot selectedCard;
    private int selectedCardIndex;
    public TextMeshProUGUI selectedCardName;
    public TextMeshProUGUI selectedCardDescription;
    public GameObject useButton;
    public GameObject unUseButton;
    public GameObject dropButton;
    public CardObject cardObject;

    private int curUsedIndex;

    public static UnitManagement instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        cardObject = GetComponent<CardObject>();
    }

    private void Start()
    {
        //slots = new CardSlot[uiSlots.Length];
        //usedSlots = new UsedCardSlot[usedUISlots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new CardSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }
        for (int i = 0; i < usedSlots.Length; i++)
        {
            usedSlots[i] = new UsedCardSlot();
            usedUISlots[i].index = i;
            usedUISlots[i].Clear();
        }

        ClearSelectedCardWindow();
        cardObject.OnAddCard();
    }

    public void AddCard(CardData card)
    {
        CardSlot emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.card = card;
            UpdateUI();
            return;
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].card != null)
                uiSlots[i].Set(slots[i]);
            else
                uiSlots[i].Clear();
        }
        if (uiSlots[selectedCardIndex].used == true)
        {
            for (int i = 0; i < usedSlots.Length; i++)
            {
                if (usedSlots[i].card == null)
                    usedUISlots[i].Set(usedSlots[i]);
                
            }
        }
    }

    CardSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].card == null)
                return slots[i];
        }
        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].card == null)
            return;
        selectedCard = slots[index];
        selectedCardIndex = index;

        selectedCardName.text = selectedCard.card.displayName;
        selectedCardDescription.text = selectedCard.card.description;

        useButton.SetActive(selectedCard.card.used == false);
        unUseButton.SetActive(selectedCard.card.used == true);
        dropButton.SetActive(true);
    }

    private void ClearSelectedCardWindow()
    {
        selectedCard = null;
        selectedCardName.text = string.Empty;
        selectedCardDescription.text = string.Empty;


        useButton.SetActive(false);
        unUseButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void OnUseButton()
    {
        if (uiSlots[curUsedIndex].used && usedSlots.Length>=usedUISlots.Length)
        {
            UnUse(curUsedIndex);
        }

        uiSlots[selectedCardIndex].used = true;
        curUsedIndex = selectedCardIndex;
        UpdateUI();
        SelectItem(selectedCardIndex);
    }

    void UnUse(int index)
    {
        uiSlots[index].used = false;
        UpdateUI();

        if (selectedCardIndex == index)
        {
            SelectItem(index);
        }
    }

    public void OnUnUseButton()
    {
        UnUse(selectedCardIndex);
    }

    public void OnDropButton()
    {
        RemoveSelectedItem();
    }

    public void OnBackButton()
    {
        //SceneManager.LoadScene();
    }

    private void RemoveSelectedItem()
    {
        if (uiSlots[selectedCardIndex].used)
        {
            UnUse(selectedCardIndex);
        }

        selectedCard.card = null;
        ClearSelectedCardWindow();
        UpdateUI();
    }

}
