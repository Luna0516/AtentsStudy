using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyShooter : EnemyBase
{
    /// <summary>
    /// 총알 발사 패턴으로 넘어갔는지 확인용
    /// </summary>
    private bool isReady = false;

    /// <summary>
    /// 처음 멈추는 시간
    /// </summary>
    public float stopTime;

    /// <summary>
    /// 총알 발사 시간
    /// </summary>
    public float fireDelay;

    /// <summary>
    /// 시간 확인용 변수
    /// </summary>
    private float elapsedTime;

    /// <summary>
    /// 보간 속도
    /// </summary>
    public float speedFactor;

    /// <summary>
    /// 실제 이동에 사용할 변수
    /// </summary>
    private float speed;

    /// <summary>
    /// 목표물 => 플레이어
    /// </summary>
    Transform target;

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(FirstMove());
    }

    private void Update()
    {
        // 준비 상태가 아니면 이동 로직 x
        if (!isReady) { return; }

        elapsedTime += Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, target.position.y), Time.deltaTime * speed);

        if (elapsedTime > fireDelay)
        {
            StartCoroutine(Fire());

            elapsedTime = 0;
        }
    }

    /// <summary>
    /// 총알을 발사하는 함수
    /// </summary>
    private IEnumerator Fire()
    {
        speed = 0;

        Vector2 firPos = (Vector2)transform.position + Vector2.left * 0.5f;
        GameObject bullet;
        bullet = Factory.Inst.GetObject(PoolObjectType.ShooterBullet, firPos + (Vector2.left * 0.5f));
        bullet.transform.up = -transform.right;
        bullet = Factory.Inst.GetObject(PoolObjectType.ShooterBullet, firPos + Vector2.up * Random.Range(0.1f, 0.5f));
        bullet.transform.up = -transform.right;
        bullet = Factory.Inst.GetObject(PoolObjectType.ShooterBullet, firPos + Vector2.up * Random.Range(-0.1f, -0.5f));
        bullet.transform.up = -transform.right;

        yield return new WaitForSeconds(1.0f);

        speed = speedFactor;
    }

    /// <summary>
    /// 생성한 뒤 앞으로 이동하는 로직
    /// </summary>
    /// <returns></returns>
    private IEnumerator FirstMove()
    {
        float moveTime = 0;

        while (moveTime < stopTime)
        {
            transform.position += Time.deltaTime * -transform.right * moveSpeed;

            moveTime += Time.deltaTime;
            yield return null;
        }

        target = GameManager.Inst.Player.transform;

        speed = speedFactor;

        isReady = true;
    }
}
