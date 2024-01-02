using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    /// <summary>
    /// 초기화 할 이동 거리
    /// </summary>
    public float repositionDistance = 20.4f;

    private void Update()
    {
        // 내 위치.x가 -repositionDistance가 되면 원래 위치로 돌아가기
        if (transform.position.x < -repositionDistance)
        {
            transform.localPosition += Vector3.right * repositionDistance * 2;

            MoveInit();
        }
    }

    /// <summary>
    /// 위치를 이동 시킨 뒤 변경할 내용들
    /// </summary>
    protected virtual void MoveInit()
    {

    }
}
