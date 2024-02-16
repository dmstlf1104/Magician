using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDestroyType { kill = 0, Arrive}
public class EnemyMove : MonoBehaviour
{
    private int wayPointCount; //이동경로 개수
    private Transform[] wayPoints; //이동경로 정보
    private int currentIndex = 0; //목표지점 인덱스
    private Movement2D movement2D; //오브젝트 이동제어
    private SpriteRenderer rend;
    private EnemySpawner enemySpawner;
    [SerializeField] private int mana = 10;

    public void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    public void Setup(EnemySpawner enemySpawner,Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;
        

        //이동경로 waypoint 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints  = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        //적위치 첫번째 waypoint 지정
        transform.position = wayPoints[currentIndex].position;

        //적 이동/목표지점 설정 코루틴 함수

        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {

            //적과 목표위치의 거리 확인후 실행
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position)< 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }
    private void NextMoveTo()
    {
        //아직 이동할 waypoint가 있으면
        if(currentIndex < wayPointCount - 1)
        {
            //적 위치를 목표위치로 정확하게 설정
            transform.position = wayPoints[currentIndex].position;
            // 이동방향 다음목표지점으로 설정
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        //지금이 마지막 waypoint면
        else
        {
            mana = 0;
            OnDie(EnemyDestroyType.Arrive);
        }
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Left")
        {
            //Debug.Log("왼쪽");
            rend.flipX = true;
        }
        
        if(coll.gameObject.tag == "Right")
        {
            //Debug.Log("오른쪽");
            rend.flipX = false;
        }
    }

    public void OnDie(EnemyDestroyType type)
    {
        enemySpawner.DestroyEnemy(type,this,mana);
    }
}
