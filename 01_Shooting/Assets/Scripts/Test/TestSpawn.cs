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
}
