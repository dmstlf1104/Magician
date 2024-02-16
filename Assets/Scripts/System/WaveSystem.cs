using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private EnemySpawner EnemySpawner;
    private int currentWaveIndex = -1; //���� ���̺�

    public int CurrentWave => currentWaveIndex + 1;
    public int MaxWave => waves.Length;
    public void StartWave()
    {
        //�ʿ� ���̾��� wave�� ����������
        if(EnemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            currentWaveIndex++;
            EnemySpawner.StartWave(waves[currentWaveIndex]);
        }
    }
} 
[System.Serializable] public struct Wave
{
    public float spawnTime; //�����ֱ�
    public int maxEnemyCount; //���������
    public GameObject[] enemyPrefabs; //�� ��������
}
