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
        InitU();        
    }
    void InitU()
    {
        foreach(Unit unit in ShopManager.Instance.User.OwnedUnits)
        {
            BuyUnit buyUnit = Instantiate(ownedItem, content).GetComponent<BuyUnit>();
            buyUnit.InitU(unit);
        }
    }    
}
