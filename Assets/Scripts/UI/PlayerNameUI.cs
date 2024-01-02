using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameUI : MonoBehaviour
{
    /// <summary>
    /// 플레이어 이름을 입력하는 컴포넌트
    /// </summary>
    private TextMeshProUGUI playerName;

    private void Awake()
    {
        playerName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (GameManager.Inst != null)
        {
            GameManager.Inst.onChangePlayerName += RefreshPlayerName;

            RefreshPlayerName(GameManager.Inst.PlayerName);
        }
    }

    private void OnDisable()
    {
        if (GameManager.Inst != null)
        {
            GameManager.Inst.onChangePlayerName -= RefreshPlayerName;
        }
    }

    /// <summary>
    /// 플레이어 이름 표시 리프레쉬
    /// </summary>
    /// <param name="newName"></param>
    private void RefreshPlayerName(string newName)
    {
        playerName.text = $"{newName}";
    }
}
