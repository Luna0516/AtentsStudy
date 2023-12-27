using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifePanel : MonoBehaviour
{
    /// <summary>
    /// 플레이어의 목숨 표시용 TextMeshPro
    /// </summary>
    TextMeshProUGUI lifeText;

    private void Awake()
    {
        lifeText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (GameManager.Inst != null)
        {
            // 플레이어의 델리게이트와 연결
            GameManager.Inst.Player.onLifeCountChange += ChangePlayerLifePanel;
        }
    }

    private void ChangePlayerLifePanel(int life)
    {
        lifeText.text = $"{life}";
    }
}
