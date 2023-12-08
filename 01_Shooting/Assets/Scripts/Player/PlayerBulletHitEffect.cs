using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletHitEffect : PoolObject
{
    /// <summary>
    /// 이펙트가 지속되는 시간
    /// </summary>
    private float hitEffectTime;

    // 컴포넌트
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        hitEffectTime = anim.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(LifeTime(hitEffectTime));
    }
}
