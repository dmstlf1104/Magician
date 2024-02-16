using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private TowerTemplate[] towerTemplate;
    [SerializeField] private EnemySpawner enemySpawner; //적 정보
    [SerializeField] private PlayerMP playerMP; //타워 건설시 마나감소
    [SerializeField] private SystemTextViewer systemTextViewer; //시스템메세지
    private bool isOnTowerBtn = false; //타워건설버튼 체크
    private bool isReadySpawn = false;
    private GameObject followTowerClone = null;
    private int towerType; //타워속성


    public void Update()
    {
        if (isReadySpawn == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isOnTowerBtn = false;
                Destroy(followTowerClone);
            }
        }
    }
    public void ReadyToSpawnTower(int type)
    {
        towerType = type;
        isReadySpawn = true;
        //버튼중복클릭방지
        if(isOnTowerBtn == true)
        {
            return;
        }
        //타워 건설여부 확인
        if (towerTemplate[towerType].weapon[0].cost > playerMP.CurrentMana)
        {
            systemTextViewer.PrintText(SystemType.Mana);
            return;
        }

        isOnTowerBtn = true;
        followTowerClone = Instantiate(towerTemplate[towerType].followTowerPrefab);

    }
    
    public void SpawnTower(Transform tileTransform)
    {
        if(isOnTowerBtn == false)
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

        isOnTowerBtn = false;
        tile.IsBuildTower = true;
        playerMP.CurrentMana -= towerTemplate[towerType].weapon[0].cost; //마나감소
        //타워 z축 높여서 먼저선택
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerTemplate[towerType].towerPrefab, position, Quaternion.identity);
        clone.GetComponent<TowerAttack>().Setup(enemySpawner, playerMP,tile);
        //임시타워삭제
        Destroy(followTowerClone);
    }
}

