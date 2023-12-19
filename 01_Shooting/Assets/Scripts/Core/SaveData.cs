using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 저장 데이터 / Serializable : 직렬화 표시 attribute
/// </summary>
[System.Serializable]
public class SaveData
{
    /// <summary>
    /// 랭킹에 저장할 이름
    /// </summary>
    public string[] rankerNames;

    /// <summary>
    /// 랭킹에 저장할 점수
    /// </summary>
    public int[] score;
}