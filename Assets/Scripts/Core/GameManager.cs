using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 난이도
/// </summary>
public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

/// <summary>
/// 게임 상태
/// </summary>
public enum GameState
{
    Main,
    Play,
    Pause,
    End,
}

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 게임 난이도
    /// </summary>
    private Difficulty difficulty;

    /// <summary>
    /// 게임 난이도 변경 프로퍼티
    /// </summary>
    public Difficulty Difficulty
    {
        get => difficulty;
        set
        {
            if (difficulty != value)
            {
                difficulty = value;
            }
        }
    }

    /// <summary>
    /// 게임 현재 상태 => 마우스 커서 조절용
    /// </summary>
    private GameState gameState;

    /// <summary>
    /// 마우스 커서 조절용 프로퍼티
    /// </summary>
    public GameState GameState
    {
        get => gameState;
        set
        {
            if (gameState != value)
            {
                gameState = value;

                // 게임 상태에 따라 마우스 커서 숨기기 -> 일단 취소
                switch (gameState)
                {
                    case GameState.Play:
                        //Cursor.visible = false;
                        Time.timeScale = 1;
                        break;
                    case GameState.Pause:
                        //Cursor.visible = true;
                        Time.timeScale = 0;
                        break;
                    case GameState.Main:
                    case GameState.End:
                    default:
                        //Cursor.visible = true;
                        Time.timeScale = 1;
                        break;
                }
            }
        }
    }

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

                // 점수 변경되면 신호 보내기 (델리게이트)
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

                // 플레이어 이름이 바뀌면 신호 보내기 (델리게이트)
                onChangePlayerName?.Invoke(playerName);
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
    /// 일시정지 창이 켜져있는지 확인용 변수
    /// </summary>
    public bool onPasuePanel = false;

    /// <summary>
    /// 메뉴얼 창이 켜져있는지 확인용 변수
    /// </summary>
    public bool onManualPanel = false;

    /// <summary>
    /// 게임이 끝나면 신호보내기 (델리게이트) (파라메터 : 게임 클리어 여부)
    /// </summary>
    public System.Action<bool> onGameEnd;

    /// <summary>
    /// 게임 일시정지 되면 신호 보내기 (파라메터 : 게임 일시정지 여부)
    /// </summary>
    public System.Action<bool> onGamePasue;

    /// <summary>
    /// 게임 메뉴얼 버튼이 클릭 되면 신호 보내기 (파라메터 : 게임 메뉴얼 표시 여부)
    /// </summary>
    public System.Action<bool> onGameManual;

    /// <summary>
    /// 점수가 바뀔때마다 신호를 보낼 델리게이트
    /// </summary>
    public System.Action<int> onChangeScore;

    /// <summary>
    /// 플레이어의 이름이 바뀔때마다 신호를 보낼 델리게이트
    /// </summary>
    public System.Action<string> onChangePlayerName;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        player = FindObjectOfType<Player>();
    }

    /// <summary>
    /// 게임 씬에 들어오면 초기화 할 요소들
    /// </summary>
    public void Init()
    {
        // 안하면 이전 씬에 있던 델리게이트 연결되어서 초기화 해줘야 한다
        onGameEnd = null;
        onChangeScore = null;
        onChangePlayerName = null;
        onGamePasue = null;
        onGameManual = null;

        onPasuePanel = false;
        onManualPanel = false;

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
