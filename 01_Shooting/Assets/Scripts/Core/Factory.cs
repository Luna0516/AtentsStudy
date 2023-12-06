using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    PlayerBulletPool playerBulletPool;

    protected override void OnInitialize()
    {
        playerBulletPool = GetComponentInChildren<PlayerBulletPool>();

        playerBulletPool.Initialize();
    }
}
