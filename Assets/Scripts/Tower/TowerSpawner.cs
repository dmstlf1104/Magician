using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private TowerTemplate towerTemplate;
    //[SerializeField] private GameObject towerPrefab;
    //[SerializeField] private int towerBuildMana = 50; //타워 건솔 소모 마나\
    [SerializeField] private EnemySpawner enemySpawner; //적 정보
    [SerializeField] private PlayerMP playerMP; //타워 건설시 마나감소
    [SerializeField] private SystemTextViewer systemTextViewer; //시스템메세지
    public void SpawnTower(Transform tileTransform)
    {
        if (towerTemplate.weapon[0].cost > playerMP.CurrentMana)
        {
            return;
        }
        Tile tile = tileTransform.GetComponent<Tile>();
        //건설여부확인
        if(tile.IsBuildTower == true)
        {
            systemTextViewer.PrintText(SystemType.Build);
            return;
        }
        tile.IsBuildTower = true;
        playerMP.CurrentMana -= towerTemplate.weapon[0].cost; //마나감소
        //타워 z축 높여서 먼저선택
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerTemplate.towerPrefab, position, Quaternion.identity);
        clone.GetComponent<TowerAttack>().Setup(enemySpawner, playerMP,tile);
    }
}

