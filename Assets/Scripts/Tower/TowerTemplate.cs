using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class TowerTemplate : ScriptableObject
{
    public GameObject towerPrefab;
    public GameObject followTowerPrefab; //�ӽ�Ÿ�� ������
    public Weapon[] weapon;

    [System.Serializable]
    public struct Weapon
    {
        public Sprite sprite; //Ÿ���̹���
        public float damage;
        public float rate;
        public float range;
        public int cost;
        public int sell; //�Ǹ�
    }
}