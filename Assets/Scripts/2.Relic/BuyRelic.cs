using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyRelic : MonoBehaviour
{
    [SerializeField] private Image relicImage;
    [SerializeField] private TMP_Text relicEx;
    [SerializeField] private TMP_Text goldValue;
    [SerializeField] private Button buyBtn;
    [SerializeField] private GameObject buyCompletePanel;

    private Relic relic;
    private ShopManager shopManager;

    private void Awake()
    {
        shopManager = ShopManager.Instance;
    }

    public void InitR(Relic relic)
    {
        this.relic = relic;

        relicImage.sprite = relic.Data.ObjectSprite;
        relicEx.text = $"<size=150%>{relic.Data.Name}</size>\n유물 효과 : {relic.Data.detailEx}";
        goldValue.text = relic.Data.Price.ToString();
        buyBtn.onClick.AddListener(() => OnBuyCompletePanel(relic));
    }

    void OnBuyCompletePanel(Relic relic)
    {
        this.relic = relic;

        if (shopManager.User.gold >= relic.Data.Price)
        {
            shopManager.User.gold -= relic.Data.Price;
            buyCompletePanel.SetActive(true);
            shopManager.PlayerGold.text = shopManager.User.gold.ToString();
            RelicStatSum(); //유물 구매시 플레이어 능력치에 유물 능력치 추가
        }
        else
        {
            shopManager.RelicNotMoney.SetActive(true);
        }

    }

    void RelicStatSum()
    {
        shopManager.User.Stat.atk += relic.Data.Stat.atk;
        shopManager.User.Stat.atkS += relic.Data.Stat.atkS;
        shopManager.User.Stat.crt += relic.Data.Stat.crt;
        shopManager.User.Stat.def += relic.Data.Stat.def;
        shopManager.User.Stat.msp += relic.Data.Stat.msp;
        shopManager.User.Stat.slow += relic.Data.Stat.slow;
        shopManager.User.Stat.stun += relic.Data.Stat.stun;
        shopManager.User.Stat.range += relic.Data.Stat.range;
    }
}
