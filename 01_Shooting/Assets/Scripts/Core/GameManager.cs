using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 게임 점수
    /// </summary>
    private int score;

    /// <summary>
    /// 스코어 변경 및 세팅 용 프로퍼티 (점수 변화 델리게이트 신호 보내기)
    /// </summary>
    public int Score
    {
        get => score;
        set
        {
            if (score != value)
            {
                score = value;

                onChangeScore?.Invoke(score);
            }
        }
    }

    /// <summary>
    /// 플레이어 이름
    /// </summary>
    private string playerName = "None";

    /// <summary>
    /// 플레이어 이름 설정, 확인용 프로퍼티
    /// </summary>
    public string PlayerName
    {
        get => playerName;
        set
        {
            if (playerName != value)
            {
                playerName = value;
            }
        }
    }

    /// <summary>
    /// 플레이어
    /// </summary>
    private Player player;

    /// <summary>
    /// 플레이어 참조용 프로퍼티
    /// </summary>
    public Player Player
    {
        get
        {
            // 없으면 현재 씬에서 찾아서 반환
            if (player == null)
            {
                player = FindObjectOfType<Player>();
            }

            return player;
        }
    }
    
    /// <summary>
    /// 점수가 바뀔때마다 신호를 보낼 델리게이트
    /// </summary>
    public System.Action<int> onChangeScore;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        player = FindObjectOfType<Player>();
    }

    /// <summary>
    /// 점수를 보유한 게임오브젝트가 죽으면 실행할 함수
    /// </summary>
    /// <param name="enemyScore">게임 오브젝트가 보유한 점수</param>
    public void AddScore(int enemyScore)
    {
        Score += enemyScore;
    }
}
