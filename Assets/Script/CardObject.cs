using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    public CardData card;


    public void OnAddCard()
    {
        UnitManagement.instance.AddCard(card);
    }
}
