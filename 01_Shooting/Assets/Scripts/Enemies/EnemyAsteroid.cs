using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsteroid : EnemyBase
{
    /// <summary>
    /// 회전 방향
    /// </summary>
    public float rotDir = 1;

    /// <summary>
    /// 회전 속도
    /// </summary>
    public float rotateSpeed;

    /// <summary>
    /// 연산에 사용될 회전 속도
    /// </summary>
    private float rotSpeed;

    /// <summary>
    /// 이동 방향
    /// </summary>
    private Vector3 moveDir = Vector3.zero;

    protected override void OnEnable()
    {
        base.OnEnable();

        float rand = Random.Range(1.0f, 3.0f);
        moveSpeed = rand;
        rotSpeed = rotateSpeed * rand;

        rotDir = Random.Range(-1.0f, 1.0f);
    }

    private void Update()
    {
        transform.position += Time.deltaTime * moveDir * moveSpeed;
        transform.Rotate(Vector3.forward, Time.deltaTime * rotSpeed * rotDir);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        moveDir = Vector3.zero;
    }

    public void SetMoveDirection(Vector3 spawnPos)
    {
        Vector3 destination = new Vector3(-10, Random.Range(-5.0f, 5.0f), 0);

        moveDir = (destination - spawnPos).normalized;
    }
}
