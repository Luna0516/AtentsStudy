using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : ItemBase, IPower
{
    /// <summary>
    /// 파워 상승 수치
    /// </summary>
    private float powerUpValue = 0.01f;

    /// <summary>
    /// 총알 발사 속도 줄여주는 값
    /// </summary>
    public float PowerUpValue
    {
        get => powerUpValue;
    }

    /// <summary>
    /// 이동 속도
    /// </summary>
    private float moveSpeed;

    /// <summary>
    /// 이동 방향
    /// </summary>
    private Vector3 moveDir = Vector3.left;

    private void Awake()
    {
        Type = ItemType.PowerUp;
    }

    private void OnEnable()
    {
        Vector3 destination = new Vector3(-10, Random.Range(-3.0f, 3.0f), 0);

        moveDir = (destination - transform.position).normalized;

        moveSpeed = Random.Range(1, 3);
    }

    private void Update()
    {
        transform.position += Time.deltaTime * moveDir * moveSpeed;
    }
}
