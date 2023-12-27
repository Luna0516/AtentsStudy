using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultyDropdown : MonoBehaviour
{
    /// <summary>
    /// 드롭다운 컴포넌트
    /// </summary>
    TMP_Dropdown d_Dropdown;

    private void Awake()
    {
        d_Dropdown = GetComponent<TMP_Dropdown>();

        d_Dropdown.onValueChanged.AddListener(ChangeDifficulty);
    }

    /// <summary>
    /// 드롭다운을 통해 값을 변경하면 게임매니저의 난이도를 바꿔줄 함수
    /// </summary>
    /// <param name="value"></param>
    private void ChangeDifficulty(int value)
    {
        GameManager.Inst.Difficulty = (Difficulty)value;
    }
}
