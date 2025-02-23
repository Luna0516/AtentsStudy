using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : EnemyBase
{
    /// <summary>
    /// 위아래 이동 범위
    /// </summary>
    [Header("상하 이동 범위")]
    public float amplitude = 3.0f;

    /// <summary>
    /// 위아래로 왕복하는 속도
    /// </summary>
    [Header("상하 왕복 속도")]
    public float frequency = 2.0f;

    /// <summary>
    /// 경과 시간
    /// </summary>
    private float timeElapsed = 0.0f;

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
            if(spawnY == Default_Pos)
            {
                spawnY = value;
            }
        }
    }

    /// <summary>
    /// 이동 벡터
    /// </summary>
    private Vector3 moveVec;

    protected override void OnDisable()
    {
        base.OnDisable();

        // 생성 위치 초기화
        spawnY = Default_Pos;
    }

    private void Update()
    {
        // 사인 함수에서 사용할 파라메터 계산
        timeElapsed += Time.deltaTime * frequency;
    }

    private void FixedUpdate()
    {
        moveVec.x = transform.position.x - Time.fixedDeltaTime * moveSpeed;
        moveVec.y = spawnY + Mathf.Sin(timeElapsed) * amplitude;

        // 위아래 운동값 계산한 위치 대입
        transform.position = moveVec;
    }
}
