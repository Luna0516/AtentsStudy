using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestSpawn : TestBase
{
    public Spawner spawner;

    protected override void Test1(InputAction.CallbackContext context)
    {
        Factory.Inst.GetObject(PoolObjectType.EnemyOrigin, transform.position);
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        Factory.Inst.GetObject(PoolObjectType.EnemyWave, transform.position);
    }

    protected override void Test3(InputAction.CallbackContext context)
    {
        Factory.Inst.GetObject(PoolObjectType.EnemyCurve, transform.position);
    }

    protected override void Test4(InputAction.CallbackContext context)
    {
        Factory.Inst.GetObject(PoolObjectType.EnemyStraight, transform.position);
    }

    protected override void Test5(InputAction.CallbackContext context)
    {
        Factory.Inst.GetObject(PoolObjectType.Enemyshooter, transform.position);
    }

    protected override void Test6(InputAction.CallbackContext context)
    {
        Factory.Inst.GetEnemyBullet(EnemyBulletType.Missile, transform.position, 6);
    }

    protected override void Test7(InputAction.CallbackContext context)
    {
        Factory.Inst.GetAsteroid(transform.position);
    }
}
