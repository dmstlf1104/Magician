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
    [SerializeField] private float attackRate; //공속
    [SerializeField] private float attackRange; //사거리
    private AttackState attackState = AttackState.SearchTarget; //무기상태
    private Transform attackTarget = null; //공격대상
    private EnemySpawner enemySpawner; //적 정보
    
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
        //이전 재생상태 종료
        StopCoroutine(attackState.ToString());
        //상태변경
        attackState = newState;
        //새로운 재생
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
            //제일 가까이 있는적 찾기 위해 최초거리 크게
            float closestDistSqr = Mathf.Infinity;
            //enemylist에 있는 맵에 존재하는 모든적 검사
            for(int i = 0; i < enemySpawner.EnemyList.Count; i++)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                //거리가 범위내에 있고 검사한 적보다 거리가 가까우면
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
            // target검사
            if (attackTarget == null)
            {
                ChangeState(AttackState.SearchTarget);
                break;
            }
            //공격범위 안 검사
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if(distance > attackRange)
            {
                attackTarget = null;
                ChangeState(AttackState.SearchTarget);
                break;
            }
            // 공속 대기
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
