using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    None,
    PlayerBullet,
}

public class Factory : Singleton<Factory>
{
    PlayerBulletPool playerBulletPool;

    protected override void OnInitialize()
    {
        playerBulletPool = GetComponentInChildren<PlayerBulletPool>();

        playerBulletPool.Initialize();
    }

    public GameObject GetObject(PoolObjectType type, Transform spawn = null)
    {
        GameObject result = null;

        switch (type)
        {
            case PoolObjectType.PlayerBullet:
                result = playerBulletPool.GetObject(spawn).gameObject;
                break;
            case PoolObjectType.None:
            default:
                break;
        }

        return result;
    }
}
