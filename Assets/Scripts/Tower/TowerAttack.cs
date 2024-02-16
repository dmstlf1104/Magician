using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public enum WeaponType {Cannon = 0,}
public enum AttackState { SearchTarget = 0, TryAttackCannon, }
public class TowerAttack : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField] private TowerTemplate towerTemplate; //타워정보
    [SerializeField] private Transform spawnPoint; //발사체 위치
    [SerializeField] private WeaponType weaponType; //무기 속성

    [Header("Cannon")]
    [SerializeField] private GameObject bulletTilePrefab; //발사체 프리팹

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
            attackTarget = FindClosestAttackTarget();

            if(attackTarget != null)
            {
                ChangeState(AttackState.TryAttackCannon);
            }
            yield return null;
        }
    }
    private IEnumerator TryAttackCannon()
    {
        while(true)
        {
            if(IsPossibleToAttackTarget() == false)
            {
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

    private Transform FindClosestAttackTarget()
    {
        //제일 가까이있는 적찾기
        float closestDistSqr = Mathf.Infinity;

        for(int i = 0; i< enemySpawner.EnemyList.Count; ++i)
        {
            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position,transform.position);

            if(distance < towerTemplate.weapon[level].range && distance <= closestDistSqr)
            {
                closestDistSqr = distance;
                attackTarget = enemySpawner.EnemyList[i].transform;
            }
        }
        return attackTarget;
    }

    private bool IsPossibleToAttackTarget()
    {
        if(attackTarget == null)
        {
            return false;
        }

        float distance = Vector3.Distance(attackTarget.position, transform.position);
        if(distance > towerTemplate.weapon[level].range)
        {
            attackTarget = null;
            return false;
        }
        return true;
    }

}
