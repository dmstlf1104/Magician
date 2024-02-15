using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public enum AttackState { SearchTarget = 0, AttackToTarget }
public class TowerAttack : MonoBehaviour
{
    [SerializeField] private GameObject bulletTilePrefab;
    [SerializeField] private Unit unitData;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float attackRate; //����
    [SerializeField] private float attackRange; //��Ÿ�
    private AttackState attackState = AttackState.SearchTarget; //�������
    private Transform attackTarget = null; //���ݴ��
    private EnemySpawner enemySpawner; //�� ����
    
    private void Awake()
    {
        attackRate = unitData.Data.Stat.atkS + ShopManager.Instance.User.Stat.atkS;
        attackRange = unitData.Data.Stat.range + ShopManager.Instance.User.Stat.range;
    }
    public void Setup(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;

        ChangeState(AttackState.SearchTarget);
    }
    public void ChangeState(AttackState newState)
    {
        //���� ������� ����
        StopCoroutine(attackState.ToString());
        //���º���
        attackState = newState;
        //���ο� ���
        StartCoroutine(attackState.ToString());
    }
    public void Update()
    {
        if(attackTarget != null)
        {
            RotateToTarget();
        }
    }
    private void RotateToTarget()
    {
        float dx = attackTarget.position.x - transform.position.x;
        float dy = attackTarget.position.y - transform.position.y;

        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0,degree);
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {
            //���� ������ �ִ��� ã�� ���� ���ʰŸ� ũ��
            float closestDistSqr = Mathf.Infinity;
            //enemylist�� �ִ� �ʿ� �����ϴ� ����� �˻�
            for(int i = 0; i < enemySpawner.EnemyList.Count; i++)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                //�Ÿ��� �������� �ְ� �˻��� ������ �Ÿ��� ������
                if(distance <= attackRange && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    attackTarget = enemySpawner.EnemyList[i].transform;
                }
            }
            if(attackTarget != null)
            {
                ChangeState(AttackState.AttackToTarget);
            }
            yield return null;
        }
    }
    private IEnumerator AttackToTarget()
    {
        while(true)
        {
            // target�˻�
            if (attackTarget == null)
            {
                ChangeState(AttackState.SearchTarget);
                break;
            }
            //���ݹ��� �� �˻�
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if(distance > attackRange)
            {
                attackTarget = null;
                ChangeState(AttackState.SearchTarget);
                break;
            }
            // ���� ���
            yield return new WaitForSeconds(attackRate);

            SpawnBulletTile();
        }
    }
    private void SpawnBulletTile()
    {
        GameObject clone = Instantiate(bulletTilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<Bullet>().Setup(attackTarget);
        //Instantiate(bulletTilePrefab, spawnPoint.position, Quaternion.identity);
    }
}
