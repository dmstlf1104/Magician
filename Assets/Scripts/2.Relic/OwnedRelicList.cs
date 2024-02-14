using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedRelicList : MonoBehaviour
{
    [Header("User OwnedRelic")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject ownedItem;

    private void Start()
    {        
        InitR();
    }    

    void InitR()
    {
        foreach (Relic relic in ShopManager.Instance.User.OwnedRelics)
        {
            BuyRelic buyRelic = Instantiate(ownedItem, content).GetComponent<BuyRelic>();
            buyRelic.InitR(relic);
        }
    }
}
