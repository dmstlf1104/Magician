using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyUnit : MonoBehaviour
{
    [SerializeField] private TMP_Text objectName;
    [SerializeField] private Image thumbnail;
    [SerializeField] private Button detailExBtn;
    [SerializeField] private GameObject detailExPanel;
    [SerializeField] [TextArea] private TMP_Text detailExTxt;
    [SerializeField] private TMP_Text objectTxt;
    //[SerializeField] private Button buyBtn;

    bool isPanelActive = false;

    private Unit unit;

    public void Init(Unit unit)
    {
        this.unit = unit;

        objectName.text = unit.Data.Name;
        thumbnail.sprite = unit.Data.ObjectSprite;
        detailExTxt.text = unit.Data.detailEx;
        objectTxt.text = $"공격력 : {unit.Data.Stat.atk.ToString()}     치명타 : {unit.Data.Stat.crt.ToString()}\n공격속도 : {unit.Data.Stat.atkS.ToString()}\n가격 : {unit.Data.Price.ToString()} Gold";
        detailExBtn.onClick.AddListener(OndetailExPanel);
    }

    void OndetailExPanel()
    {
        isPanelActive = !isPanelActive;
        detailExPanel.SetActive(isPanelActive);
    }
}
