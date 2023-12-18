using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpread : EnemyBase
{
    /// <summary>
    /// 이동중일 때는 총알을 발사하지 못하게 하는 변수
    /// </summary>
    private bool isReady;

    /// <summary>
    /// 총알 발사 방향 지정용 변수
    /// </summary>
    private int count = 0;
    
    /// <summary>
    /// 총알 발사 쿨타임
    /// </summary>
    public float fireDelay;

    /// <summary>
    /// 총알 속도
    /// </summary>
    public float bulletSpeed = 1.5f;

    /// <summary>
    /// 멈춰서 총알 발사하기 전까지의 시간
    /// </summary>
    public float apperTime;
    
    /// <summary>
    /// 쿨타임 확인용 시간 변수
    /// </summary>
    private float elapsedTime;

    /// <summary>
    /// 이동 연산에 사용할 속도 변수
    /// </summary>
    private float speed;

    /// <summary>
    /// 총알 발사 시작시점의 자신의 위치
    /// </summary>
    private Vector2 myPos;

    /// <summary>
    /// 발사 방향 1
    /// </summary>
    private Vector2[] firVec1 = new Vector2[] { new Vector2(2, 0), new Vector2(-2, 0), new Vector2(0, 2), new Vector2(0, -2),
                                                new Vector2(2, 2), new Vector2(-2, 2), new Vector2(-2, -2), new Vector2(2, -2) };

    /// <summary>
    /// 발사 방향 2
    /// </summary>
    private Vector2[] firVec2 = new Vector2[] { new Vector2(2, 1), new Vector2(2, -1), new Vector2(1, 2), new Vector2(1, -2),
                                                new Vector2(-2, 1), new Vector2(-2, -1), new Vector2(-1, 2), new Vector2(-1, -2) };

    private void Awake()
    {
        // 단위벡터로 바꾸기
        // foreach는 readOnly로 되어서 바꿀 수 없다고 한다...

        for (int i = 0; i < firVec1.Length; i++)
        {
            firVec1[i] = firVec1[i].normalized;
        }

        for (int i = 0; i < firVec2.Length; i++)
        {
            firVec2[i] = firVec2[i].normalized;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // 속도 원래대로 초기화
        speed = moveSpeed;
        isReady = false;

        // 코루틴 실행 => 이동후 총알을 발사하기 위해
        StartCoroutine(AppearanceCorou());
    }

    private void Update()
    {
        if (isReady)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > fireDelay)
            {
                SpawnBullet();

                elapsedTime = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position += Time.fixedDeltaTime * Vector3.left * speed;
    }

    /// <summary>
    /// 총알 생성 함수 (8 방향으로 생성)
    /// </summary>
    private void SpawnBullet()
    {
        if(count == 0)
        {
            foreach(Vector2 firePos in firVec1)
            {
                GameObject bullet = Factory.Inst.GetEnemyBullet(EnemyBulletType.Base, (myPos + firePos), bulletSpeed);
                bullet.transform.up = firePos;
            }
        }
        else
        {
            foreach (Vector2 firePos in firVec2)
            {
                GameObject bullet = Factory.Inst.GetEnemyBullet(EnemyBulletType.Base, (myPos + firePos), bulletSpeed);
                bullet.transform.up = firePos;
            }
        }

        count = (count + 1) % 2;
    }

    /// <summary>
    /// 총알 생성하기 전까지의 이동을 하게할 코루틴
    /// </summary>
    private IEnumerator AppearanceCorou()
    {
        yield return new WaitForSeconds(apperTime);
        speed = Stop_Speed;
        myPos = transform.position;

        isReady = true;
    }
}
