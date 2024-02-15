using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;
    private float damage;

    public void Setup(Transform target, float damage)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target; //타워가 설정한 타겟
        this.damage = damage; //타워가 설정한 공격력
    }
    private void Update()
    {
        if(target != null) //타겟이 있으면
        {
            //발사체를 타겟까지 이동
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        { 
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) { return; } //적이 아닌대상과 부딫힐때
        if (collision.transform != target) { return; } //타겟아아닐때

        //collision.GetComponent<EnemyMove>().OnDie();
        collision.GetComponent<EnemyHp>().TakeDamage(damage); //적체력감소
        Destroy(gameObject);
    }
}
