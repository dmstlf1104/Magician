using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Status Stat; //유물 스탯 더하기용
    public int gold;
    public List<Unit> Inven = new List<Unit>();
    public List<Unit> SellUnits = new List<Unit>();
    public List<Relic> OwnedRelics = new List<Relic>();    
}

[System.Serializable]
public class Unit
{      
    public UnitData Data;
}

[System.Serializable]
public class Relic
{    
    public RelicData Data;
}


public class ShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text Price;
    public TMP_Text PlayerGold;
    public GameObject UnitNotMoney;
    public GameObject RelicNotMoney;
    public Button EvolutionBtn;

    public static ShopManager Instance;    

    public UserData User;
    Unit unit;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;        
    }

    private void Start()
    {
        User.gold = 20000;
        PlayerGold.text = User.gold.ToString();
    }   

    public void SceneChange()
    {
        SceneManager.LoadScene("UnitManagerScene");
    }
}
