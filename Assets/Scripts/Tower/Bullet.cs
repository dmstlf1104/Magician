using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;

    public void Setup(Transform target)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target; //타워가 설정한 타겟
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
        if (!collision.CompareTag("Enemy")) { return; }
        if (collision.transform != target) { return; }

        collision.GetComponent<EnemyMove>().OnDie();
        Destroy(gameObject);
    }
}
