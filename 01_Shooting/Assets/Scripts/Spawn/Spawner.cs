using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Spawner : MonoBehaviour
{
    /// <summary>
    /// 스폰 X 좌표
    /// </summary>
    [Header("스폰되는 x 좌표")]
    public float spawnX;

    /// <summary>
    /// 스폰되는 넓이
    /// </summary>
    [Header("스폰되는 높이(half)")]
    public float halfHeight;

    /// <summary>
    /// 스포너에 사용할 레벨별 스폰데이터
    /// </summary>
    public SpawnerLevelData[] spawnerLevelDatas;

    /// <summary>
    /// 보스 소환을 위한 경과시간 체크용 변수
    /// </summary>
    private float elapsedTime = 0;

    /// <summary>
    /// 보스 소환되었는지 확인하는 변수
    /// </summary>
    private bool isBossSpawn;

    private void Start()
    {
        List<SpawnData> spawnDatas = new List<SpawnData>();

        if (GameManager.Inst != null) {
            int arrayLength = spawnerLevelDatas.Length;
            for(int i = 0; i < arrayLength; i++)
            {
                if (spawnerLevelDatas[i].difficulty == GameManager.Inst.Difficulty)
                {
                    spawnDatas = spawnerLevelDatas[i].spawnDatas;
                    break;
                }
            }
        }

        if (spawnDatas.Count > 0)
        {
            foreach (var spawnData in spawnDatas)
            {
                StartCoroutine(SpawnCoroutine(spawnData));
            }

            if (GameManager.Inst != null)
            {
                GameManager.Inst.Player.onDie += () =>
                {
                    StopAllCoroutines();
                };
            }
        }
    }

    private void OnEnable()
    {
        isBossSpawn = false;
    }

    private void Update()
    {
        if (!isBossSpawn)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > 60)
            {
                isBossSpawn = true;
                StopAllCoroutines();
                Factory.Inst.GetObject(PoolObjectType.EnemyBoss, new Vector3(spawnX, 0.0f));
            }
        }
    }

    /// <summary>
    /// 적 생성 함수 (미리 지정한 데이터에 따라 생성)
    /// </summary>
    /// <param name="type"></param>
    private void Spawn(PoolObjectType type)
    {
        Vector3 spawnPos = new Vector3(spawnX, Random.Range(-halfHeight, halfHeight), 0);

        switch (type)
        {
            // 스프레드 적은 따로 3마리씩 소환
            case PoolObjectType.EnemySpread:
                Factory.Inst.GetObject(type, new Vector3(spawnX - 2.5f, 0));
                Factory.Inst.GetObject(type, new Vector3(spawnX, 2.5f));
                Factory.Inst.GetObject(type, new Vector3(spawnX, -2.5f));
                break;
            case PoolObjectType.EnemyOrigin:
            case PoolObjectType.EnemyWave:
            case PoolObjectType.EnemyShooter:
            case PoolObjectType.EnemyCurve:
            case PoolObjectType.EnemyStraight:
                Factory.Inst.GetObject(type, spawnPos);
                break;
            case PoolObjectType.EnemyAsteroid:
                Factory.Inst.GetAsteroid(spawnPos);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 게임이 시작하면 실행시킬 적 소환 코루틴
    /// </summary>
    /// <param name="data">스폰할 적 데이터</param>
    private IEnumerator SpawnCoroutine(SpawnData data)
    {
        while (true)
        {
            yield return new WaitForSeconds(data.interval);
            Spawn(data.type);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // 왼쪽 위
        Vector2 p0 = new Vector2(spawnX - 0.5f, halfHeight);
        // 오른쪽 위
        Vector2 p1 = new Vector2(spawnX + 0.5f, halfHeight);
        // 왼쪽 아래
        Vector2 p2 = new Vector2(spawnX - 0.5f, -halfHeight);
        // 오른쪽 아래
        Vector2 p3 = new Vector2(spawnX + 0.5f, -halfHeight);

        Handles.color = Color.yellow;

        Handles.DrawLine(p0, p1);
        Handles.DrawLine(p1, p3);
        Handles.DrawLine(p3, p2);
        Handles.DrawLine(p2, p0);
    }
#endif
}
