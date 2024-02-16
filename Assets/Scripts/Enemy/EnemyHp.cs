using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] private float maxHP; //최대체력
    private float currentHP; //현재체력
    private bool isDie = false; //적 사망상태
    private EnemyMove enemyMove;
    private SpriteRenderer spriteRenderer;
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    private void Awake()
    {
        currentHP = maxHP;
        enemyMove = GetComponent<EnemyMove>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (isDie == true) return;

        currentHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        if(currentHP <= 0)
        {
            isDie = true;

            enemyMove.OnDie(EnemyDestroyType.kill);
        }
    }
    private IEnumerator HitAlphaAnimation()
    {
        Color color = spriteRenderer.color;

        color.a = 0.4f;
        spriteRenderer.color = color;

        yield return new WaitForSeconds(0.05f);

        color.a = 1.0f;
        spriteRenderer.color = color;
    }
}
