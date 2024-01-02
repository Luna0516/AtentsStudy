using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Spawn Level", menuName = "ScriptableObject/SpawnData", order = 0)]
public class SpawnerLevelData : ScriptableObject
{
    /// <summary>
    /// 게임 난이도
    /// </summary>
    [Header("게임 난이도")]
    public Difficulty difficulty;

    /// <summary>
    /// 스폰 데이터(리스트형)
    /// </summary>
    [Header("스폰 데이터들")]
    public List<SpawnData> spawnDatas;
}
