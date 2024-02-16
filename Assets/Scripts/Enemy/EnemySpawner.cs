using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField] private GameObject enemyPrefab; //�� ������
    [SerializeField] private GameObject enemyHPSliderPrefab; //�� ü���� ��Ÿ���� Slider UI ������
    [SerializeField] private Transform canvasTransform; //ĵ���� Ʈ������
    //[SerializeField] private float spawnTime; //�����ֱ�
    [SerializeField] private Transform[] wayPoints; // ���� �������� �̵����
    [SerializeField] private PlayerHP playerHP; //�÷��̾� ü��
    [SerializeField] private PlayerMP playerMP; 
    private Wave currentWave; //���̺� ����
    private int currentEnemyCount; //���̺꿡 �����ִ� �� ����
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
            // ���̺꿡 �����ϴ� �� ������ �������ϰ�� �������� ����
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            EnemyMove enemy = clone.GetComponent<EnemyMove>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);

            SpawnEnemyHPSlider(clone); //ü�� ui
            //���̺� ������ �� ���� +1
            spawnEnemyCount++;
            //�����ð����
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
