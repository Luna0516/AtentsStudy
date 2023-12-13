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
    SpreadBullet,
    ShooterBullet,
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
    SpreadBulletPool spreadBulletPool;
    ShooterBulletPool shooterBulletPool;

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
        spreadBulletPool = GetComponentInChildren<SpreadBulletPool>();
        shooterBulletPool = GetComponentInChildren<ShooterBulletPool>();
        enemyShooterPool = GetComponentInChildren<EnemyShooterPool>();

        playerBulletPool.Initialize();
        playerBulletHitEffectPool.Initialize();
        explosionEffectPool.Initialize();
        enemyOriginPool.Initialize();
        enemyWavePool.Initialize();
        enemyCurvePool.Initialize();
        enemyStraightPool.Initialize();
        enemySpreadPool.Initialize();
        spreadBulletPool.Initialize();
        shooterBulletPool.Initialize();
        enemyShooterPool.Initialize();
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
            case PoolObjectType.SpreadBullet:
                result = spreadBulletPool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.ShooterBullet:
                result = shooterBulletPool.GetObject(spawn).gameObject;
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
            case PoolObjectType.None:
            case PoolObjectType.PlayerBullet:
            case PoolObjectType.PlayerBulletHitEffect:
            case PoolObjectType.ExplosionEffect:
            case PoolObjectType.EnemyOrigin:
            case PoolObjectType.EnemyStraight:
            case PoolObjectType.EnemySpread:
            case PoolObjectType.Enemyshooter:
            case PoolObjectType.SpreadBullet:
            case PoolObjectType.ShooterBullet:
            default:
                break;
        }

        result.transform.position = spawnPos;

        return result;
    }
}
