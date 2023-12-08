using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : PoolObject
{
    /// <summary>
    /// 이펙트가 지속되는 시간
    /// </summary>
    private float explosionEffectTime;

    // 컴포넌트
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        explosionEffectTime = anim.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(LifeTime(explosionEffectTime));
    }
}
