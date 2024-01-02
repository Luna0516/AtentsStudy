using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    /// <summary>
    /// 캔버스 그룹 컴포넌트
    /// </summary>
    CanvasGroup canvas;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (GameManager.Inst != null)
        {
            // 게임이 종료되면 창 활성화
            GameManager.Inst.onGamePasue += SetActiveCanvas;
        }

        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }

    /// <summary>
    /// 캔버스 그룹을 통해 상태창 띄우는 함수
    /// </summary>
    private void SetActiveCanvas(bool clear)
    {
        if (clear)
        {
            canvas.alpha = 1;
            canvas.blocksRaycasts = true;
            canvas.interactable = true;
        }
        else
        {
            canvas.alpha = 0;
            canvas.blocksRaycasts = false;
            canvas.interactable = false;
        }
    }
}
