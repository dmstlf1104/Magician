using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private TowerTemplate[] towerTemplate;
    [SerializeField] private EnemySpawner enemySpawner; //�� ����
    [SerializeField] private PlayerMP playerMP; //Ÿ�� �Ǽ��� ��������
    [SerializeField] private SystemTextViewer systemTextViewer; //�ý��۸޼���
    private bool isOnTowerBtn = false; //Ÿ���Ǽ���ư üũ
    private bool isReadySpawn = false;
    private GameObject followTowerClone = null;
    private int towerType; //Ÿ���Ӽ�


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
        //��ư�ߺ�Ŭ������
        if(isOnTowerBtn == true)
        {
            return;
        }
        //Ÿ�� �Ǽ����� Ȯ��
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
        //�Ǽ�����Ȯ��
        if(tile.IsBuildTower == true)
        {
            systemTextViewer.PrintText(SystemType.Build);
            return;
        }

        isOnTowerBtn = false;
        tile.IsBuildTower = true;
        playerMP.CurrentMana -= towerTemplate[towerType].weapon[0].cost; //��������
        //Ÿ�� z�� ������ ��������
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerTemplate[towerType].towerPrefab, position, Quaternion.identity);
        clone.GetComponent<TowerAttack>().Setup(enemySpawner, playerMP,tile);
        //�ӽ�Ÿ������
        Destroy(followTowerClone);
    }
}

