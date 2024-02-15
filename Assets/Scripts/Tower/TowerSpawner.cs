using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private TowerTemplate towerTemplate;
    //[SerializeField] private GameObject towerPrefab;
    //[SerializeField] private int towerBuildMana = 50; //Ÿ�� �Ǽ� �Ҹ� ����\
    [SerializeField] private EnemySpawner enemySpawner; //�� ����
    [SerializeField] private PlayerMP playerMP; //Ÿ�� �Ǽ��� ��������
    [SerializeField] private SystemTextViewer systemTextViewer; //�ý��۸޼���
    public void SpawnTower(Transform tileTransform)
    {
        if (towerTemplate.weapon[0].cost > playerMP.CurrentMana)
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
        tile.IsBuildTower = true;
        playerMP.CurrentMana -= towerTemplate.weapon[0].cost; //��������
        //Ÿ�� z�� ������ ��������
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerTemplate.towerPrefab, position, Quaternion.identity);
        clone.GetComponent<TowerAttack>().Setup(enemySpawner, playerMP,tile);
    }
}

