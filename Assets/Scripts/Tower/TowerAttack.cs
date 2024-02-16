using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public enum WeaponType {Cannon = 0,Laser,}
public enum AttackState { SearchTarget = 0, TryAttackCannon, TryAttackLaser, }
public class TowerAttack : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField] private TowerTemplate towerTemplate; //Ÿ������
    [SerializeField] private Transform spawnPoint; //�߻�ü ��ġ
    [SerializeField] private WeaponType weaponType; //���� �Ӽ�

    [Header("Cannon")]
    [SerializeField] private GameObject bulletTilePrefab; //�߻�ü ������

    [Header("Laser")]
    [SerializeField] private LineRenderer lineRenderer; //��������
    [SerializeField] private Transform  hitEffect; //Ÿ��ȿ��
    [SerializeField] private LayerMask targetLayer; //������ �΋H���� ���̾� ����

    private int level = 0; //����
    private AttackState attackState = AttackState.SearchTarget; //�������
    private Transform attackTarget = null; //���ݴ��
    private EnemySpawner enemySpawner; //�� ����
    private SpriteRenderer spriteRenderer; //�̹��� �����
    private PlayerMP playerMP; //�÷��̾� ���� ȹ�漳��
    private Tile ownerTile; //��ġ����Ÿ��

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
            attackTarget = FindClosestAttackTarget();

            if(attackTarget != null)
            {
                if(weaponType == WeaponType.Cannon)
                {
                    ChangeState(AttackState.TryAttackCannon);
                }
                else if(weaponType == WeaponType.Laser)
                {
                    ChangeState(AttackState.TryAttackLaser);
                }
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
            // ���� ���
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
    
    private IEnumerator TryAttackLaser()
    {
        EnableLaser();

        while (true)
        {
            //Ÿ�پ���
            if(IsPossibleToAttackTarget() == false)
            {
                //Ÿ��ȿ�� ��Ȱ��ȭ
                DisableLaser();
                ChangeState(AttackState.SearchTarget);
                break;
            }

            SpawnLaser();

            yield return null;
        }
    }

    public bool Upgrade()
    {
        if(playerMP.CurrentMana < towerTemplate.weapon[level + 1].cost)
        {
            return false;
        }
        level++;
        //Ÿ������
        spriteRenderer.sprite = towerTemplate.weapon[level].sprite;
        //Ÿ�����
        playerMP.CurrentMana -= towerTemplate.weapon[level].cost;
        if(weaponType == WeaponType.Laser)
        {
            //������� �������� ����
            lineRenderer.startWidth = 0.05f + level * 0.05f;
            lineRenderer.endWidth = 0.05f;
        }
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
        //���� �������ִ� ��ã��
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

    private void EnableLaser()
    {
        lineRenderer.gameObject.SetActive(true);
        hitEffect.gameObject.SetActive(true);
    }
    private void DisableLaser()
    {
        lineRenderer.gameObject.SetActive(false);
        hitEffect.gameObject.SetActive(false);
    }
    private void SpawnLaser()
    {
        Vector3 direction = attackTarget.position - spawnPoint.position;
        RaycastHit2D[] hit = Physics2D.RaycastAll(spawnPoint.position, direction, towerTemplate.weapon[level].range,targetLayer);

        for(int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform == attackTarget)
            {
                //��������
                lineRenderer.SetPosition(0, spawnPoint.position);
                //��ǥ����
                lineRenderer.SetPosition(1, new Vector3(hit[i].point.x, hit[i].point.y, 0) + Vector3.back);
                //ȿ��  ��ġ
                hitEffect.position = hit[i].point;
                //ü�°���
                attackTarget.GetComponent<EnemyHp>().TakeDamage(towerTemplate.weapon[level].damage * Time.deltaTime);
            }
        }
    }
}
