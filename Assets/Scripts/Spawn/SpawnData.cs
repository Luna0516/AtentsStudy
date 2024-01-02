using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]   // 클래스 직렬화 => 인스펙터에서 편집 위해서
public class SpawnData
{
    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="_type">스폰될 오브젝트 타입</param>
    /// <param name="_interval">스폰되는 시간 간격(쿨타임)</param>
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
