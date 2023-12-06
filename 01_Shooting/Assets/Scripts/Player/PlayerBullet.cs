using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : PoolObject
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    [Header("이동 속도")]
    public float moveSpeed;

    private void FixedUpdate()
    {
        transform.position += Time.fixedDeltaTime * Vector3.right * moveSpeed;
    }
}
