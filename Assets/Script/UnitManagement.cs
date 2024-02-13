using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static UnityEditor.Progress;


public class UnitManagement : MonoBehaviour
{
    public CardSlotUI[] uiSlots;
    public static CardData[] cardSlots = new CardData[18];

    public UsedCardSoltUI[] usedUISlots;
    public static CardData[] usedCardSlots = new CardData[5];

    [Header("Selected Card")]
    private CardData selectedCard;
    private int selectedCardIndex;
    public TextMeshProUGUI selectedCardName;
    public TextMeshProUGUI selectedCardDescription;
    public GameObject useButton;
    public GameObject unUseButton;
    public GameObject dropButton;

    private int curUsedIndex;

    public static UnitManagement instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 18; i++)
        {
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }
        for (int i = 0; i < 5; i++)
        {
            usedUISlots[i].Set();
        }
        ClearSelectedCardWindow();
    }

    public void AddCard(CardData card)
    {
        CardData emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot = card;
            UpdateUI();
            return;
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (cardSlots[i] != null)
                uiSlots[i].Set(cardSlots[i]);
            else
                uiSlots[i].Clear();
        }
    }

    CardData GetEmptySlot()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (cardSlots[i] == null)
                return cardSlots[i];
        }
        return null;
    }

    public void SelectItem(int index)
    {
        if (cardSlots[index] == null)
            return;
        selectedCard = cardSlots[index];
        selectedCardIndex = index;

        selectedCardName.text = selectedCard.displayName;
        selectedCardDescription.text = selectedCard.description;

        useButton.SetActive(selectedCard.used == false);
        unUseButton.SetActive(selectedCard.used == true);
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
        cardSlots[selectedCardIndex].used = true;
        curUsedIndex = selectedCardIndex;
        SelectItem(selectedCardIndex);
        UpdateUI();
    }

    void UnUse(int index)
    {
        cardSlots[index].used = false;
        UpdateUI();
        if (selectedCardIndex == index)
        {
            SelectItem(index);
        }
    }

    public void OnUnUseButton()
    {
        cardSlots[selectedCardIndex].used = false;
        curUsedIndex = selectedCardIndex;
        SelectItem(selectedCardIndex);
        UpdateUI();
        
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
        if (cardSlots[selectedCardIndex].used)
        {
            UnUse(selectedCardIndex);
        }

        selectedCard = null;
        ClearSelectedCardWindow();
        UpdateUI();
    }

}
