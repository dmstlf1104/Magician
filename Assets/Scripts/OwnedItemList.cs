using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedItemList : MonoBehaviour
{
    [Header("User Owned")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject ownedItem;

    private void Start()
    {
        Init();
    }
    void Init()
    {
        foreach(Unit unit in ShopManager.Instance.User.OwnedUnits)
        {
            BuyUnit buyUnit = Instantiate(ownedItem, content).GetComponent<BuyUnit>();
            buyUnit.Init(unit);
        }
    }

    void InitR()
    {
        foreach (Unit unit in ShopManager.Instance.User.OwnedRelics)
        {
            BuyUnit buyUnit = Instantiate(ownedItem, content).GetComponent<BuyUnit>();
            buyUnit.Init(unit);
        }
    }
}
