using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : PoolObject
{
    /// <summary>
    /// 적이 살아있는지 확인용 변수
    /// </summary>
    protected bool isAlive;

    /// <summary>
    /// 위치 초기화용 상수
    /// </summary>
    protected const float Default_Pos = -10000.0f;

    /// <summary>
    /// 이동속도 0으로 만드는 상수
    /// </summary>
    protected const float Stop_Speed = 0.0f;

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed;

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
            if (health != value && isAlive)
            {
                health = value;
                if (health <= 0)
                {
                    isAlive = false;
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
    protected int score;

    /// <summary>
    /// 죽을 때 점수 계산해서 보내기
    /// </summary>
    protected int Score
    {
        get
        {
            // 점수 설정
            score = (maxHealth * maxHealth) + 5;

            return score;
        }
    }
    
    /// <summary>
    /// health가 0이하로 내려가면 실행할 델리게이트 (파라메터 : 점수)
    /// </summary>
    public System.Action<int> onDie;

    /// <summary>
    /// 아이템 나올 확률
    /// </summary>
    private int probability = 10;

    /// <summary>
    /// 확률 배열
    /// </summary>
    protected int[] probabilityArray;

    protected virtual void Awake()
    {
        probabilityArray = new int[100];

        int arrayLength = probabilityArray.Length;

        for(int i = 0;i< arrayLength; i++)
        {
            probabilityArray[i] = i + 1;
        }
    }

    protected virtual void OnEnable()
    {
        // 게임 난이도 별 보정 수치
        int correction = 1;
        
        if (GameManager.Inst)
        {
            correction = (int)GameManager.Inst.Difficulty + 1;
            onDie += GameManager.Inst.AddScore;
        }

        // 현재 체력을 최대 체력으로 설정
        Health = maxHealth * correction;

        isAlive = true;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        onDie = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Health--;
        }
    }

    /// <summary>
    /// 죽었을 때 실행할 함수
    /// </summary>
    protected virtual void Die()
    {
        // 터지는 이펙트 생성
        Factory.Inst.GetObject(PoolObjectType.ExplosionEffect, transform.position);

        // 셔플 함수로 배열 섞기
        Utile.Shuffle(probabilityArray);
        if (probabilityArray[0] <= probability)
        {
            Factory.Inst.GetObject(PoolObjectType.ItemPowerUp, transform.position);
        }

        onDie?.Invoke(Score);

        gameObject.SetActive(false);
    }
}
