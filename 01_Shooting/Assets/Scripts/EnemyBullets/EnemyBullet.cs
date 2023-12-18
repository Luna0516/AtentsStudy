using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBulletType
{
    Base,
    Shooter,
}

public class EnemyBullet : PoolObject
{
    /// <summary>
    /// 총알의 이동속도
    /// </summary>
    private float moveSpeed;

    /// <summary>
    /// 총알의 이동속도 설정용 프로퍼티
    /// </summary>
    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            if(moveSpeed != value)
            {
                moveSpeed = value;
            }
        }
    }

    /// <summary>
    /// 총알 이미지들
    /// </summary>
    public Sprite[] sprites;

    /// <summary>
    /// 내 총알의 종류
    /// </summary>
    private EnemyBulletType bulletType = EnemyBulletType.Base;

    /// <summary>
    /// Factory에서 생성하면서 바꿀 이미지
    /// </summary>
    public EnemyBulletType BulletType
    {
        set
        {
            if (bulletType != value)
            {
                bulletType = value;
                spriteRenderer.sprite = sprites[(int)bulletType];
            }
        }
    }

    // 컴포넌트들
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        transform.position += Time.fixedDeltaTime * transform.up * MoveSpeed;
    }
}
