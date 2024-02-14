using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedUnitList : MonoBehaviour
{
    [Header("User OwnedUnit")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject ownedItem;

    private void Start()
    {
        InitOUList();
    }
    void InitOUList()
    {
        foreach (Unit unit in ShopManager.Instance.User.SellUnits)
        {
            OwnedUnit ownedUnit = Instantiate(ownedItem, content).GetComponent<OwnedUnit>();
            ownedUnit.InitOU(unit);
        }
    }
}
