using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status
{
    public int atk;
    public int atkS;
    public int crt;
    public int def;
    public int msp;
    public int slow;
    public int stun;

    public Status(int atk, int crt, int def, int msp, int atkS)
    {
        this.atk = atk;
        this.crt = crt;
        this.def = def;
        this.msp = msp;
        this.atkS = atkS;
    }
}

[System.Serializable]
public class UserData
{
    public List<Unit> OwnedUnits = new List<Unit>();
    public List<Relic> OwnedRelics = new List<Relic>();
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
public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public UserData User;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
