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
