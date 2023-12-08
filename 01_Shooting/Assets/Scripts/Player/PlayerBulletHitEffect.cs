using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletHitEffect : PoolObject
{
    /// <summary>
    /// 이펙트가 지속되는 시간
    /// </summary>
    [Header("이펙트 시간")]
    public float hitEffectTime;

    private void OnEnable()
    {
        StartCoroutine(LifeTime(hitEffectTime));
    }
}
