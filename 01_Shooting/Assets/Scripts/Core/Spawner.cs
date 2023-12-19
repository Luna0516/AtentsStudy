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

    [System.Serializable]
    public struct SpawnData
    {
        public SpawnData(PoolObjectType _type, float _interval)
        {
            type = _type;
            interval = _interval;
        }

        /// <summary>
        /// 스폰시킬 타입
        /// </summary>
        [Header("스폰할 적 타입")]
        public PoolObjectType type;

        /// <summary>
        /// 스폰되는 시간 (쿨타임)
        /// </summary>
        [Header("스폰되는 시간")]
        public float interval;
    }

    /// <summary>
    /// 스폰 될 데이터
    /// </summary>
    [Header("스폰 데이터들")]
    public List<SpawnData> spawnDatas;

    private void Start()
    {
        foreach (var spawnData in spawnDatas)
        {
            StartCoroutine(SpawnCoroutine(spawnData));
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
