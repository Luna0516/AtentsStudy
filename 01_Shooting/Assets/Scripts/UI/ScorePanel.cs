using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePanel : MonoBehaviour
{
    /// <summary>
    /// 현재 점수
    /// </summary>
    private float currentScore = 0;

    /// <summary>
    /// 바뀔 점수
    /// </summary>
    private int changeScore = 0;

    /// <summary>
    /// 점수가 올라가는 속도
    /// </summary>
    public float scoreUpSpeed = 50.0f;

    /// <summary>
    /// 점수 변경용 TextMeshPro
    /// </summary>
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if(GameManager.Inst != null)
        {
            // 스코어 점수 초기화
            currentScore = GameManager.Inst.Score;
            changeScore = GameManager.Inst.Score;
            GameManager.Inst.onChangeScore += RefreshScore;

            RefreshScoreText();
        }
    }

    private void Update()
    {
        if (currentScore < changeScore)
        {
            // 점수 변화의 차이가 클수록 증가 속도 빠르게
            float speed = Mathf.Max((changeScore - currentScore) * 5.0f, scoreUpSpeed);

            currentScore += Time.deltaTime * speed;
            currentScore = Mathf.Min(currentScore, changeScore);
            RefreshScoreText();
        }
        else
        {
            // 점수차이가 없으면 오류 방지를 위해 점수를 똑같게 맞추기
            currentScore = changeScore;
            RefreshScoreText();
        }
    }

    /// <summary>
    /// 변경된 점수 표시하는 UI의 Text 변경 함수
    /// </summary>
    private void RefreshScoreText()
    {
        // 소수점 이하 자리수 0
        scoreText.text = $"{currentScore:f0}";
    }

    /// <summary>
    /// 플레이어 점수 변경시 바뀔 점수를 변경할 함수
    /// </summary>
    /// <param name="score">바뀔 점수</param>
    private void RefreshScore(int score)
    {
        changeScore = score;
    }
}
