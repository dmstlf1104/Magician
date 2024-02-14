using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellUnitList : MonoBehaviour
{
    [Header("User SellUnit")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject ownedItem;

    private void Start()
    {
        InitU();        
    }
    void InitU()
    {
        foreach(Unit unit in ShopManager.Instance.User.SellUnits)
        {
            SellUnit sellUnit = Instantiate(ownedItem, content).GetComponent<SellUnit>();
            sellUnit.InitU(unit);
        }
    }    
}
