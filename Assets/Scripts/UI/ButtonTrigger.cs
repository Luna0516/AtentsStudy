using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
    /// <summary>
    /// 버튼의 종류 enum
    /// </summary>
    public enum ButtonType
    {
        None,
        Start,      // 게임시작
        Pause,      // 일시정지
        UnPause,    // 일시정지 해제
        Manual,     // 조작방법
        OffManual,  // 조작방법 해제
        Exit,       // 게임종료
        Restart,    // 다시하기
        Main,       // 메인으로
    }

    /// <summary>
    /// 이 버튼의 종류
    /// </summary>
    public ButtonType type;

    /// <summary>
    /// 메인 씬 이름
    /// </summary>
    readonly string MainSceneName = "MainScene";

    /// <summary>
    /// 게임 씬 이름
    /// </summary>
    readonly string GameSceneName = "GameScene";

    // 컴포넌트
    Button button;

    // 싱글톤 미리 받아오기 -> 너무 자주 써서
    GameManager gameManager;
    Factory factory;
    SceneHandler sceneHandler;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();

        switch (type)
        {
            case ButtonType.Start:
                button.onClick.AddListener(OnStart);
                break;
            case ButtonType.Pause:
                button.onClick.AddListener(OnPause);
                break;
            case ButtonType.UnPause:
                button.onClick.AddListener(UnPause);
                break;
            case ButtonType.Manual:
                button.onClick.AddListener(OnManual);
                break;
            case ButtonType.OffManual:
                button.onClick.AddListener(OffManual);
                break;
            case ButtonType.Exit:
                button.onClick.AddListener(OnExit);
                break;
            case ButtonType.Restart:
                button.onClick.AddListener(OnRestart);
                break;
            case ButtonType.Main:
                button.onClick.AddListener(OnMain);
                break;
            case ButtonType.None:
            default:
                break;
        }
    }

    private void Start()
    {
        gameManager = GameManager.Inst;
        factory = Factory.Inst;
        sceneHandler = SceneHandler.Inst;
    }

    /// <summary>
    /// 게임 시작을 위한 함수
    /// </summary>
    private void OnStart()
    {
        if (!gameManager.onManualPanel || gameManager.onPasuePanel)
        {
            factory.DisableEnemy();
            sceneHandler.NextSceneName = GameSceneName;
            gameManager.GameState = GameState.Play;
        }
    }

    /// <summary>
    /// 게임 일시 정지 함수
    /// </summary>
    private void OnPause()
    {
        if (!gameManager.onPasuePanel || gameManager.onManualPanel)
        {
            button.interactable = false;
            gameManager.onPasuePanel = true;
            gameManager.onGamePasue?.Invoke(true);
            gameManager.GameState = GameState.Pause;
        }
    }

    /// <summary>
    /// 게임 일시 정지 해제 함수
    /// </summary>
    private void UnPause()
    {
        if (gameManager.onPasuePanel)
        {
            button.interactable = true;
            gameManager.onPasuePanel = false;
            gameManager.onGamePasue?.Invoke(false);
            gameManager.GameState = GameState.Play;
        }
    }

    /// <summary>
    /// 메뉴얼 창을 여는 함수
    /// </summary>
    private void OnManual()
    {
        if (!gameManager.onManualPanel)
        {
            gameManager.onManualPanel = true;
            gameManager.onGameManual?.Invoke(true);
        }
    }

    /// <summary>
    /// 메뉴얼 창을 닫는 함수
    /// </summary>
    private void OffManual()
    {
        if (gameManager.onManualPanel)
        {
            gameManager.onManualPanel = false;
            gameManager.onGameManual?.Invoke(false);
        }
    }

    /// <summary>
    /// 게임 종료 함수
    /// </summary>
    private void OnExit()
    {
        Application.Quit();
    }

    /// <summary>
    /// 현재 씬의 게임을 다시 시작하기
    /// </summary>
    private void OnRestart()
    {
        factory.DisableEnemy();
        sceneHandler.NextSceneName = GameSceneName;
        gameManager.GameState = GameState.Play;
    }

    /// <summary>
    /// 처음 씬으로 돌아가는 함수
    /// </summary>
    private void OnMain()
    {
        factory.DisableEnemy();
        sceneHandler.NextSceneName = MainSceneName;
        gameManager.GameState = GameState.Main;
    }
}
