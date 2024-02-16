using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDataViewer : MonoBehaviour
{
    [SerializeField] private Image imageTower;
    [SerializeField] private TextMeshProUGUI textDamage;
    [SerializeField] private TextMeshProUGUI textRate;
    [SerializeField] private TextMeshProUGUI textRange;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TowerAttackRange towerAttackRange;
    [SerializeField] private Button buttonUpgrade;
    [SerializeField] private SystemTextViewer systemTextViewer;

    private TowerAttack currentTower;
    private void Awake()
    {
        OffPanel();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform towerAttack)
    {
        currentTower = towerAttack.GetComponent<TowerAttack>();
        //타워 정보 on
        gameObject.SetActive(true);

        UpdateTowerData();

        //공격범위
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
    }

    public void OffPanel()
    {
        gameObject.SetActive(false);
        towerAttackRange.OffAttackRange();
    }

    private void UpdateTowerData()
    {
        textDamage.text = "Damage : " + currentTower.Damage;
        textRate.text = "Rate : " + currentTower.Rate;
        textRange.text = "Range : " + currentTower.Range;
        textLevel.text = "Level : " + currentTower.Level;

        buttonUpgrade.interactable = currentTower.Level < currentTower.MaxLevel ? true : false;
    }

    public void OnClickEventTowerUpgrade()
    {
        bool isSuccess = currentTower.Upgrade();

        if(isSuccess == true)
        {
            UpdateTowerData();

            towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
        }
        else
        {
            systemTextViewer.PrintText(SystemType.Mana);
        }
    }

    public void ONClickEventTowerSell()
    {
        currentTower.Sell();
        OffPanel();
    }
}
