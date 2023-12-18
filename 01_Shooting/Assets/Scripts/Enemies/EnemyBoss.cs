using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    /// <summary>
    /// 등장을 완료했는지 확인할 변수
    /// </summary>
    private bool isReady = false;

    /// <summary>
    /// 등장 속도
    /// </summary>
    [Header("등장 속도")]
    public float appearSpeed = 2f;

    /// <summary>
    /// 총알 발사 속도
    /// </summary>
    [Header("총알 발사 속도")]
    public float fireDelay = 1.0f;

    /// <summary>
    /// 미사일 발사 속도
    /// </summary>
    [Header("미사일 발사 속도")]
    public float fireMissileDelay = 4.0f;

    /// <summary>
    /// 연산에 사용할 속도 변수
    /// </summary>
    private float speed;

    /// <summary>
    /// 보스 활동 범위(최소)
    /// </summary>
    private Vector2 areaMin;

    /// <summary>
    /// 보스 활동 범위(최대)
    /// </summary>
    private Vector2 areaMax;

    /// <summary>
    /// 목적지 벡터
    /// </summary>
    private Vector3 destination;

    /// <summary>
    /// 이동 방향 벡터
    /// </summary>
    private Vector3 moveDir;

    /// <summary>
    /// 총알 발사 위치
    /// </summary>
    private Transform firePos;

    /// <summary>
    /// 미사일 발사 위치1
    /// </summary>
    private Transform fireMissilePos1;

    /// <summary>
    /// 미사일 발사 위치2
    /// </summary>
    private Transform fireMissilePos2;

    private void Awake()
    {
        // 총알 및 미사일 발사 위치 설정
        Transform child = transform.GetChild(1);
        firePos = child.transform;
        child = transform.GetChild(2);
        fireMissilePos1 = child.transform;
        child = transform.GetChild(3);
        fireMissilePos1 = child.transform;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        isReady = false;
        speed = 0.0f;

        StartCoroutine(AppearProcess());
    }

    private void FixedUpdate()
    {
        if (!isReady) { return; }

        transform.position += Time.fixedDeltaTime * moveDir * speed;

        if (transform.position.y > areaMax.y)
        {
            SetNextDestination();
        }
        else if (transform.position.y < areaMin.y)
        {
            SetNextDestination();
        }
        else if (transform.position.x > areaMax.x)
        {
            SetNextDestination();
        }
        else if (transform.position.x < areaMin.x)
        {
            SetNextDestination();
        }
    }

    /// <summary>
    /// 목적지를 정해주는 함수
    /// </summary>
    private void SetNextDestination()
    {
        // 이동 범위 랜덤으로 구하기
        float x = Random.Range(areaMin.x, areaMax.x);
        float y = Random.Range(areaMin.y, areaMax.y);

        destination = new Vector3(x, y, 0);

        // 방향 벡터 구하기
        moveDir = destination - transform.position;
        moveDir.Normalize();
    }

    /// <summary>
    /// 총알 발사 함수
    /// </summary>
    private void FireBullet()
    {
        Debug.Log("총알 발사!");
    }

    /// <summary>
    /// 미사일 발사 함수
    /// </summary>
    private void FireMissile()
    {
        Debug.Log("미사일 발사!");
        Debug.Log("미사일 발사!");
    }

    /// <summary>
    /// 보스 등장할 때 실행할 모션
    /// </summary>
    private IEnumerator AppearProcess()
    {
        destination = new Vector3(7, 0, 0);

        Vector3 distance;

        while (true)
        {
            transform.position = Vector3.Slerp(transform.position, destination, Time.deltaTime * appearSpeed);

            yield return null;

            distance = transform.position - destination;

            if (distance.sqrMagnitude < 0.001f) { break; }
        }

        transform.position = destination;

        isReady = true;

        // 활동 범위 정하기
        areaMax = transform.position + new Vector3(1, 1.5f, 0);
        areaMin = transform.position + new Vector3(-1, -1.5f, 0);

        speed = moveSpeed;

        // 목적지 설정
        SetNextDestination();

        // 총알 및 미사일 발사 코루틴 실행
        StartCoroutine(FireBulletRoutine());
        StartCoroutine(FireMissileRoutine());
    }

    /// <summary>
    /// 총알 발사 코루틴
    /// </summary>
    private IEnumerator FireBulletRoutine()
    {
        while (true)
        {
            FireBullet();
            yield return new WaitForSeconds(fireDelay);
        }
    }

    /// <summary>
    /// 미사일 발사 코루틴
    /// </summary>
    private IEnumerator FireMissileRoutine()
    {
        while (true)
        {
            FireMissile();
            yield return new WaitForSeconds(fireMissileDelay);
        }
    }
}
