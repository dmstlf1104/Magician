using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; //�� ������
    [SerializeField] private float spawnTime; //�����ֱ�
    [SerializeField] private Transform[] wayPoints; // ���� �������� �̵����
    private List<EnemyMove> enemyList;

    public List<EnemyMove> EnemyList => enemyList;

    private void Awake()
    {
        enemyList = new List<EnemyMove>();
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemyPrefab);
            EnemyMove enemy = clone.GetComponent<EnemyMove>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);

            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void DestroyEnemy(EnemyMove enemy)
    {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
