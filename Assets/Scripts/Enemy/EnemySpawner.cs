using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField] private GameObject enemyPrefab; //적 프리팹
    [SerializeField] private GameObject enemyHPSliderPrefab; //적 체력을 나타내는 Slider UI 프리팹
    [SerializeField] private Transform canvasTransform; //캔버스 트랜스폼
    //[SerializeField] private float spawnTime; //생성주기
    [SerializeField] private Transform[] wayPoints; // 현재 스테이지 이동경로
    [SerializeField] private PlayerHP playerHP; //플레이어 체력
    [SerializeField] private PlayerMP playerMP; 
    private Wave currentWave; //웨이브 정보
    private int currentEnemyCount; //웨이브에 남아있는 적 숫자
    private List<EnemyMove> enemyList;

    public List<EnemyMove> EnemyList => enemyList;

    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => currentWave.maxEnemyCount;

    private void Awake()
    {
        enemyList = new List<EnemyMove>();
        //StartCoroutine("SpawnEnemy");
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave;
        currentEnemyCount = currentWave.maxEnemyCount;
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        int spawnEnemyCount = 0;
        while (spawnEnemyCount < currentWave.maxEnemyCount)
        {
            // 웨이브에 등장하는 적 종류가 여러개일경우 랜덤으로 등장
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            EnemyMove enemy = clone.GetComponent<EnemyMove>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);

            SpawnEnemyHPSlider(clone); //체력 ui
            //웨이브 생성한 적 숫자 +1
            spawnEnemyCount++;
            //스폰시간대기
            yield return new WaitForSeconds(currentWave.spawnTime);
        }
    }

    public void DestroyEnemy(EnemyDestroyType type,EnemyMove enemy,int mana)
    {
        if(type == EnemyDestroyType.Arrive)
        {
            playerHP.TakeDamage(1);
        }
        else if(type == EnemyDestroyType.kill)
        {
            playerMP.CurrentMana += mana;
        }

        currentEnemyCount--;
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);

        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;

        sliderClone.GetComponent<SliderPositionAutoSetter>().SetUp(enemy.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHp>());
    }
}
