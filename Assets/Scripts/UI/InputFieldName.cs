using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputFieldName : MonoBehaviour
{
    TMP_InputField n_InputField;

    private void Awake()
    {
        n_InputField = GetComponent<TMP_InputField>();

        // 입력이 끝났을 때
        n_InputField.onEndEdit.AddListener(RefreshPlayerName);
    }

    /// <summary>
    /// 인풋필드에서 플레이어 이름 입력이 끝나면 처리할 함수
    /// </summary>
    /// <param name="newName">바꿀 이름</param>
    private void RefreshPlayerName(string newName)
    {
        // 입력한 이름의 길이가 0보다 크고 GameManger가 있으면
        if (newName.Length > 0 && GameManager.Inst != null)
        {
            // 게임매니저에 있는 플레이어 이름 바꾸기
            GameManager.Inst.PlayerName = newName;

            n_InputField.text = GameManager.Inst.PlayerName;
        }
    }
}
