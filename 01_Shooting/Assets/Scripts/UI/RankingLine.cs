using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingLine : MonoBehaviour
{
    /// <summary>
    /// 이름 텍스트
    /// </summary>
    TextMeshProUGUI nameText;

    /// <summary>
    /// 점수 텍스트
    /// </summary>
    TextMeshProUGUI scoreText;

    /// <summary>
    /// 신기록 확인 new 표시
    /// </summary>
    GameObject newText;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        nameText = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2);
        scoreText = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(3);
        newText = child.gameObject;
    }

    /// <summary>
    /// 랭킹라인에 기록 표시하기위한 함수
    /// </summary>
    /// <param name="rankerName">점수 기록을 한 플레이어 이름</param>
    /// <param name="record">점수</param>
    /// <param name="newRecord">새로 갱신한 점수 유무</param>
    public void SetData(string rankerName, int record, bool newRecord = false)
    {
        NewRecore(newRecord);

        nameText.text = rankerName;

        // N0로 해야 세자리마다 콤마가 찍힌다.
        scoreText.text = record.ToString("N0");
    }

    /// <summary>
    /// 신 기록인지 알려주는 함수
    /// </summary>
    /// <param name="newRecore">신기록 여부</param>
    private void NewRecore(bool newRecore)
    {
        if (newText.activeSelf != newRecore)
        {
            newText.SetActive(newRecore);
        }
    }
}
