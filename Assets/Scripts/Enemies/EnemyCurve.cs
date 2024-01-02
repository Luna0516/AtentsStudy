using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCurve : EnemyBase
{
    /// <summary>
    /// 회전 속도
    /// </summary>
    public float rotateSpeed;

    /// <summary>
    /// 생성 y위치값
    /// </summary>
    private float spawnY = Default_Pos;

    /// <summary>
    /// 생성 위치 지정용
    /// </summary>
    public float SpawnY
    {
        set
        {
            if (spawnY == Default_Pos)
            {
                spawnY = value;

                // 스폰한 위치에 따라 회전할 방향(상하) 지정
                if(spawnY > 0)
                {
                    curveDir = 1;
                }
                else
                {
                    curveDir = -1;
                }
            }
        }
    }

    /// <summary>
    /// 커브 방향 지정 (1 = 위방향 / -1 = 아래방향)
    /// </summary>
    private int curveDir = 0;

    protected override void OnDisable()
    {
        base.OnDisable();

        // 생성 위치 초기화
        spawnY = Default_Pos;
    }

    private void FixedUpdate()
    {
        transform.position += Time.fixedDeltaTime * -transform.right * moveSpeed;
        transform.Rotate(Time.fixedDeltaTime * rotateSpeed * curveDir * Vector3.forward);
    }
}
