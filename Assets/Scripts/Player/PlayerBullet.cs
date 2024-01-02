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
        transform.position += Time.fixedDeltaTime * transform.right * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Factory.Inst.GetObject(PoolObjectType.PlayerBulletHitEffect, collision.contacts[0].point);
            gameObject.SetActive(false);
        }
    }
}
