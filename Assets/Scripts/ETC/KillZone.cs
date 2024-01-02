using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 풀오브젝트인지 확인 하고 맞으면 비활, 아니면 삭제 (문제 생길 경우를 대비)
        PoolObject obj = collision.GetComponent<PoolObject>();

        if(obj != null)
        {
            // 풀 오브젝트 비활성화
            collision.gameObject.SetActive(false);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
