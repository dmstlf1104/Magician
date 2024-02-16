using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Status
{
    public int atk;
    public float atkS;
    public int crt;
    public int def;
    public int msp;
    public int slow;
    public int stun;
    public float range;

    public Status(int atk, int crt, int def, int msp, float atkS, int slow, int stun, float range)
    {
        this.atk = atk;
        this.crt = crt;
        this.def = def;
        this.msp = msp;
        this.atkS = atkS;
        this.slow = slow;
        this.stun = stun;
        this.range = range;
    }
}

[System.Serializable]
public class UserData
{
    //public Status Stat; 유물 스탯 더하기용
    public int gold;
    public List<Unit> Inventory = new List<Unit>();
    public List<Unit> SellUnits = new List<Unit>();
    public List<Relic> OwnedRelics = new List<Relic>();
    public List<EvolutionUnit> EvolutionUnits = new List<EvolutionUnit>();
}

[System.Serializable]
public class Unit
{
    public bool IsPurchase;    
    public UnitData Data;
}

[System.Serializable]
public class Relic
{
    public bool IsPurchase;
    public RelicData Data;
}

[System.Serializable]
public class EvolutionUnit
{    
    public List<SelectEvolutionUnit> EUnits = new List<SelectEvolutionUnit>();
}

[System.Serializable]
public class SelectEvolutionUnit
{
    public bool IsPurchase;
    public EvolutionUnitData Data;
}

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text Price;
    public TMP_Text PlayerGold;
    public GameObject UnitNotMoney;
    public GameObject RelicNotMoney;

    public static ShopManager Instance;
    private SelectEvolutionUnit selectEvolutionUnit;

    public UserData User;        

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        User.gold = 20000;
        PlayerGold.text = User.gold.ToString();
    }

    public void PriceValue(SelectEvolutionUnit selectEvolutionUnit)
    {
        this.selectEvolutionUnit = selectEvolutionUnit;

        Price.text = selectEvolutionUnit.Data.Price.ToString();
    }
}
