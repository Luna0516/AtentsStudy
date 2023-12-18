using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyMissileBullet : EnemyBulletBase
{
    /// <summary>
    /// 쫓는 시간 (유도 성능을 가지는 시간)
    /// </summary>
    public float chasingTime = 1.5f;

    /// <summary>
    /// 체력
    /// </summary>
    private int health;

    /// <summary>
    /// 체력 설정 및 확인 프로퍼티
    /// </summary>
    public int Health
    {
        get => health;
        protected set
        {
            if (health != value)
            {
                health = value;
                if (health <= 0)
                {
                    Die();
                }
            }
        }
    }

    /// <summary>
    /// 최대 체력
    /// </summary>
    public int maxHealth;

    /// <summary>
    /// 죽으면 얻게 될 점수
    /// </summary>
    public int score;

    /// <summary>
    /// health가 0이하로 내려가면 실행할 델리게이트 (파라메터 : 점수)
    /// </summary>
    public System.Action<int> onDie;

    /// <summary>
    /// 움직이는 방향
    /// </summary>
    Vector3 moveDir;

    /// <summary>
    /// 목표물 ( 플레이어 )
    /// </summary>
    private Transform target;

    private void OnEnable()
    {
        Health = maxHealth;

        target = GameManager.Inst.Player.transform;

        StartCoroutine(ChaseTarget());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Health--;
        }
    }

    /// <summary>
    /// 생성시 일정 시간동안 적을 추적하여 유도 기능을 보이게 하는 코루틴
    /// </summary>
    private IEnumerator ChaseTarget()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < chasingTime)
        {
            // 타겟이 없으면 종료
            if (target == null) { yield break; }

            elapsedTime += Time.deltaTime;

            moveDir = (target.position - transform.position).normalized;

            transform.up = moveDir;

            yield return null;
        }
    }

    /// <summary>
    /// 죽었을 때 실행할 함수
    /// </summary>
    protected virtual void Die()
    {
        // 터지는 이펙트 생성
        Factory.Inst.GetObject(PoolObjectType.ExplosionEffect, transform.position);

        onDie?.Invoke(score);

        gameObject.SetActive(false);
    }
}
