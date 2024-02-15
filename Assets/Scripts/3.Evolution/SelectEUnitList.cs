using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEUnitList : MonoBehaviour
{
    [Header("User SelectEUnit")]
    [SerializeField] private Transform charSelectPanel;
    [SerializeField] private GameObject charSelectObject;
       
    private Unit unit;        

    public void InitEUList(Unit unit)
    {
        this.unit = unit;            

        if (charSelectPanel.childCount == 0)
        {            
            foreach (Unit selectEvolutionUnit in ShopManager.Instance.User.EvolutionUnits[unit.Data.UnitIndex].EUnits)
            {
                SelectEUnit selectEUnit = Instantiate(charSelectObject, charSelectPanel).GetComponent<SelectEUnit>();
                selectEUnit.InitEU(selectEvolutionUnit);                
            }            
        }
        else if (charSelectPanel.childCount >= 3)
        {
            foreach (Transform child in charSelectPanel)
            {
                Destroy(child.gameObject);
            }

            foreach (Unit selectEvolutionUnit in ShopManager.Instance.User.EvolutionUnits[unit.Data.UnitIndex].EUnits)
            {
                SelectEUnit selectEUnit = Instantiate(charSelectObject, charSelectPanel).GetComponent<SelectEUnit>();
                selectEUnit.InitEU(selectEvolutionUnit);
            }
        }
    }
}
