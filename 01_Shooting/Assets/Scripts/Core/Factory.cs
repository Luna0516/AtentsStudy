using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    None,
    PlayerBullet,
    PlayerBulletHitEffect,
    ExplosionEffect,
    EnemyOrigin,
    EnemyWave,
}

public class Factory : Singleton<Factory>
{
    PlayerBulletPool playerBulletPool;
    PlayerBulletHitEffectPool playerBulletHitEffectPool;
    ExplosionEffectPool explosionEffectPool;
    EnemyOriginPool enemyOriginPool;

    protected override void OnInitialize()
    {
        playerBulletPool = GetComponentInChildren<PlayerBulletPool>();
        playerBulletHitEffectPool = GetComponentInChildren<PlayerBulletHitEffectPool>();
        explosionEffectPool = GetComponentInChildren<ExplosionEffectPool>();
        enemyOriginPool = GetComponentInChildren<EnemyOriginPool>();

        playerBulletPool.Initialize();
        playerBulletHitEffectPool.Initialize();
        explosionEffectPool.Initialize();
        enemyOriginPool.Initialize();
    }

    public GameObject GetObject(PoolObjectType type, Transform spawn = null)
    {
        GameObject result = null;

        switch (type)
        {
            case PoolObjectType.PlayerBullet:
                result = playerBulletPool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.PlayerBulletHitEffect:
                result = playerBulletHitEffectPool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.ExplosionEffect:
                result = explosionEffectPool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.EnemyOrigin:
                result = enemyOriginPool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.None:
            default:
                break;
        }

        return result;
    }

    public GameObject GetObject(PoolObjectType type, Vector2 spawnPos)
    {
        GameObject result = GetObject(type);

        result.transform.position = spawnPos;

        return result;
    }
}
