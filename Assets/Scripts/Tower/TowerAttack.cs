using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public enum AttackState { SearchTarget = 0, AttackToTarget }
public class TowerAttack : MonoBehaviour
{
    [SerializeField] private TowerTemplate towerTemplate; //타워정보
    [SerializeField] private GameObject bulletTilePrefab;
    [SerializeField] private Transform spawnPoint;
    //[SerializeField] private float attackRate = 0.5f; //공속
    //[SerializeField] private float attackRange = 2.0f; //사거리
    //[SerializeField] private int attackDamage = 1; //공격력
    private int level = 0; //레벨
    private AttackState attackState = AttackState.SearchTarget; //무기상태
    private Transform attackTarget = null; //공격대상
    private EnemySpawner enemySpawner; //적 정보
    private SpriteRenderer spriteRenderer; //이미지 변경용
    private PlayerMP playerMP; //플레이어 마나 획득설정
    private Tile ownerTile; //배치중인타일

    public Sprite TowerSprite => towerTemplate.weapon[level].sprite;
    public float Damage => towerTemplate.weapon[level].damage;
    public float Rate => towerTemplate.weapon[level].rate;
    public float Range => towerTemplate.weapon[level].range;
    public int Level => level + 1;

    public int MaxLevel => towerTemplate.weapon.Length;
    public void Setup(EnemySpawner enemySpawner,PlayerMP playerMP,Tile ownerTile)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.enemySpawner = enemySpawner;
        this.playerMP = playerMP;
        this.ownerTile = ownerTile;
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
                if(distance <= towerTemplate.weapon[level].range && distance <= closestDistSqr)
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
            if(distance > towerTemplate.weapon[level].range)
            {
                attackTarget = null;
                ChangeState(AttackState.SearchTarget);
                break;
            }
            // 공속 대기
            yield return new WaitForSeconds(towerTemplate.weapon[level].rate);

            SpawnBulletTile();
        }
    }
    private void SpawnBulletTile()
    {
        GameObject clone = Instantiate(bulletTilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<Bullet>().Setup(attackTarget, towerTemplate.weapon[level].damage);
        //Instantiate(bulletTilePrefab, spawnPoint.position, Quaternion.identity);
    }

    public bool Upgrade()
    {
        if(playerMP.CurrentMana < towerTemplate.weapon[level + 1].cost)
        {
            return false;
        }
        level++;
        //타워외형
        spriteRenderer.sprite = towerTemplate.weapon[level].sprite;
        //타워골드
        playerMP.CurrentMana -= towerTemplate.weapon[level].cost;

        return true;
    }

    public void Sell()
    {
        playerMP.CurrentMana += towerTemplate.weapon[level].sell;
        ownerTile.IsBuildTower = false;
        Destroy(gameObject);
    }
}
