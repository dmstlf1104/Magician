using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private int wayPointCount; //�̵���� ����
    private Transform[] wayPoints; //�̵���� ����
    private int currentIndex = 0; //��ǥ���� �ε���
    private Movement2D movement2D; //������Ʈ �̵�����
    private SpriteRenderer rend;

    public void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    public void Setup(Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();
        

        //�̵���� waypoint ����
        wayPointCount = wayPoints.Length;
        this.wayPoints  = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        //����ġ ù��° waypoint ����
        transform.position = wayPoints[currentIndex].position;

        //�� �̵�/��ǥ���� ���� �ڷ�ƾ �Լ�

        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {

            //���� ��ǥ��ġ�� �Ÿ� Ȯ���� ����
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position)< 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }
    private void NextMoveTo()
    {
        //���� �̵��� waypoint�� ������
        if(currentIndex < wayPointCount - 1)
        {
            //�� ��ġ�� ��ǥ��ġ�� ��Ȯ�ϰ� ����
            transform.position = wayPoints[currentIndex].position;
            // �̵����� ������ǥ�������� ����
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        //������ ������ waypoint��
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Left")
        {
            Debug.Log("����");
            rend.flipX = true;
        }
        
        if(coll.gameObject.tag == "Right")
        {
            Debug.Log("������");
            rend.flipX = false;
        }
    }
}
