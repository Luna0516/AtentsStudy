using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 1;

    private void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * Vector3.left * moveSpeed);
    }
}
