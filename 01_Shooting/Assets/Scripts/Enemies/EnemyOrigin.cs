using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrigin : EnemyBase
{
    // 왼쪽으로 오는 적

    private void FixedUpdate()
    {
        transform.position += Time.deltaTime * Vector3.left * moveSpeed;
    }
}
