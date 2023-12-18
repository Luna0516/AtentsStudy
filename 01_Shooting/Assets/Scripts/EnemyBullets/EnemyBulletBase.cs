using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBase : PoolObject
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

    private void FixedUpdate()
    {
        transform.position += Time.fixedDeltaTime * transform.up * MoveSpeed;
    }
}
