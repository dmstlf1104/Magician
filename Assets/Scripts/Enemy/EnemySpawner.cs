using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; //적 프리팹
    [SerializeField] private float spawnTime; //생성주기
    [SerializeField] private Transform[] wayPoints; // 현재 스테이지 이동경로
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
