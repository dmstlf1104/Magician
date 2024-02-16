using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private EnemySpawner EnemySpawner;
    public bool Victory = false;
    private int currentWaveIndex = -1; //현재 웨이브

    public int CurrentWave => currentWaveIndex + 1;
    public int MaxWave => waves.Length;

    public void Update()
    {
        if (EnemySpawner.EnemyList.Count == 0 && currentWaveIndex >= waves.Length - 1)
        {
            Victory = true;
        }
    }
    public void StartWave()
    {
        //맵에 적이없고 wave가 남아있으면
        if(EnemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            currentWaveIndex++;
            EnemySpawner.StartWave(waves[currentWaveIndex]);
        }
    }
} 
[System.Serializable] public struct Wave
{
    public float spawnTime; //생성주기
    public int maxEnemyCount; //적등장숫자
    public GameObject[] enemyPrefabs; //적 등장종류
}
