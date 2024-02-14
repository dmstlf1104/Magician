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

    private Relic relic;    

    public void InitR(Relic relic)
    {
        this.relic = relic;

        relicImage.sprite = relic.Data.ObjectSprite;
        relicEx.text = $"<size=150%>{relic.Data.Name}</size>\n유물 효과 : {relic.Data.detailEx} +{relic.Data.Stat.atk}";
        goldValue.text = relic.Data.Price.ToString();        
    }

    
}
