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
        this.target = target; //Ÿ���� ������ Ÿ��
        this.damage = damage; //Ÿ���� ������ ���ݷ�
    }
    private void Update()
    {
        if(target != null) //Ÿ���� ������
        {
            //�߻�ü�� Ÿ�ٱ��� �̵�
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
        if (!collision.CompareTag("Enemy")) { return; } //���� �ƴѴ��� �΋H����
        if (collision.transform != target) { return; } //Ÿ�پƾƴҶ�

        //collision.GetComponent<EnemyMove>().OnDie();
        collision.GetComponent<EnemyHp>().TakeDamage(damage); //��ü�°���
        Destroy(gameObject);
    }
}
