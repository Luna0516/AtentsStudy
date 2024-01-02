using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBullet : PoolObject
{
    /// <summary>
    /// 총알의 이동속도
    /// </summary>
    public float moveSpeed;

    private void FixedUpdate()
    {
        transform.position += Time.fixedDeltaTime * transform.up * moveSpeed;
    }
}
