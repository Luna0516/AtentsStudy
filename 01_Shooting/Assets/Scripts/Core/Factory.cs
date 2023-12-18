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
    EnemyCurve,
    EnemyStraight,
    EnemySpread,
    Enemyshooter,
    EnemyBoss,
    EnemyBulletBase,
    EnemyShooterBullet,
    EnemyMissileBullet,
}

public enum EnemyBulletType
{
    Base,
    Shooter,
    Missile
}

public class Factory : Singleton<Factory>
{
    PlayerBulletPool playerBulletPool;
    PlayerBulletHitEffectPool playerBulletHitEffectPool;
    ExplosionEffectPool explosionEffectPool;
    EnemyOriginPool enemyOriginPool;
    EnemyWavePool enemyWavePool;
    EnemyCurvePool enemyCurvePool;
    EnemyStraightPool enemyStraightPool;
    EnemySpreadPool enemySpreadPool;
    EnemyShooterPool enemyShooterPool;
    EnemyBossPool enemyBossPool;
    EnemyBulletBasePool enemyBulletBasePool;
    EnemyShooterBulletPool enemyShooterBulletPool;
    EnemyMissileBulletPool enemyMissileBulletPool;

    protected override void OnInitialize()
    {
        playerBulletPool = GetComponentInChildren<PlayerBulletPool>();
        playerBulletHitEffectPool = GetComponentInChildren<PlayerBulletHitEffectPool>();
        explosionEffectPool = GetComponentInChildren<ExplosionEffectPool>();
        enemyOriginPool = GetComponentInChildren<EnemyOriginPool>();
        enemyWavePool = GetComponentInChildren<EnemyWavePool>();
        enemyCurvePool = GetComponentInChildren<EnemyCurvePool>();
        enemyStraightPool = GetComponentInChildren<EnemyStraightPool>();
        enemySpreadPool = GetComponentInChildren<EnemySpreadPool>();
        enemyShooterPool = GetComponentInChildren<EnemyShooterPool>();
        enemyBossPool = GetComponentInChildren<EnemyBossPool>();
        enemyBulletBasePool = GetComponentInChildren<EnemyBulletBasePool>();
        enemyShooterBulletPool = GetComponentInChildren<EnemyShooterBulletPool>();
        enemyMissileBulletPool = GetComponentInChildren<EnemyMissileBulletPool>();

        playerBulletPool.Initialize();
        playerBulletHitEffectPool.Initialize();
        explosionEffectPool.Initialize();
        enemyOriginPool.Initialize();
        enemyWavePool.Initialize();
        enemyCurvePool.Initialize();
        enemyStraightPool.Initialize();
        enemySpreadPool.Initialize();
        enemyShooterPool.Initialize();
        enemyBossPool.Initialize();
        enemyBulletBasePool.Initialize();
        enemyShooterBulletPool.Initialize();
        enemyMissileBulletPool.Initialize();
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
            case PoolObjectType.EnemyWave:
                result = enemyWavePool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.EnemyCurve:
                result = enemyCurvePool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.EnemyStraight:
                result = enemyStraightPool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.EnemySpread:
                result = enemySpreadPool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.Enemyshooter:
                result = enemyShooterPool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.EnemyBoss:
                result = enemyBossPool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.EnemyBulletBase:
                result = enemyBulletBasePool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.EnemyShooterBullet:
                result = enemyShooterBulletPool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.EnemyMissileBullet:
                result = enemyMissileBulletPool.GetObject(spawn).gameObject;
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

        switch (type)
        {
            case PoolObjectType.EnemyWave:
                EnemyWave enemyWave = result.GetComponent<EnemyWave>();
                enemyWave.SpawnY = spawnPos.y;
                break;
            case PoolObjectType.EnemyCurve:
                EnemyCurve enemyCurve = result.GetComponent<EnemyCurve>();
                enemyCurve.SpawnY = spawnPos.y;
                break;
        }

        result.transform.position = spawnPos;

        return result;
    }

    public GameObject GetEnemyBullet(EnemyBulletType type, Vector2 spawnPos, float bulletSpeed)
    {
        GameObject result = null;
        
        switch (type)
        {
            case EnemyBulletType.Shooter:
                result = GetObject(PoolObjectType.EnemyShooterBullet);
                break;
            case EnemyBulletType.Missile:
                result = GetObject(PoolObjectType.EnemyMissileBullet);
                break;
            case EnemyBulletType.Base:
            default:
                result = GetObject(PoolObjectType.EnemyBulletBase);
                break;
        }

        EnemyBulletBase enemyBullet = result.GetComponent<EnemyBulletBase>();

        enemyBullet.MoveSpeed = bulletSpeed;

        result.transform.position = spawnPos;

        return result;
    }
}
