using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStraight : EnemyBase
{
    /// <summary>
    /// 멈추기 전까지 시간
    /// </summary>
    [Header("멈추기 까지 시간")]
    public float stopTime;

    /// <summary>
    /// 멈춰 있는 시간
    /// </summary>
    [Header("멈춰서 기다리는 시간")]
    public float waitTime;

    /// <summary>
    /// 돌진 속도
    /// </summary>
    [Header("돌진 속도")]
    public float rushSpeed;

    /// <summary>
    /// 실제 이동 연산에 사용할 속도 변수
    /// </summary>
    private float speed;

    /// <summary>
    /// 회전 시간
    /// </summary>
    public float rotTime;

    /// <summary>
    /// 목표물
    /// </summary>
    Transform target;

    protected override void OnEnable()
    {
        base.OnEnable();

        speed = moveSpeed;
        target = GameManager.Inst.Player.transform;

        StartCoroutine(RushCorou());
    }

    private void FixedUpdate()
    {
        transform.position += Time.fixedDeltaTime * -transform.right * speed;
    }

    /// <summary>
    /// 생성한 뒤 이동하고난 후 목표 지정하고 돌진하는 코루틴
    /// </summary>
    private IEnumerator RushCorou()
    {
        yield return new WaitForSeconds(stopTime);

        speed = 0;

        yield return new WaitForSeconds(waitTime);

        // target을 바라보게 회전시키기

        float elapsedTime = 0;

        Vector2 directionToTarget = (target.position - transform.position).normalized;

        // 각도 구하기 (-180이유 => 적들의 스프라이트가 위쪽을 향하고 있어서 그 값까지 -90 더해줘서 최종 -180)
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 180;

        // 곱하기용으로 역수 구해놓기
        float inverseRotSpeed = 1 / rotTime;

        while (elapsedTime < rotTime)
        {
            elapsedTime += Time.deltaTime;

            float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, elapsedTime * inverseRotSpeed);
            transform.rotation = Quaternion.Euler(0, 0, angle);

            yield return null;
        }

        // 최종 정확한 회전을 보장
        transform.rotation = Quaternion.Euler(0, 0, targetAngle); 

        speed = rushSpeed;
    }
}
