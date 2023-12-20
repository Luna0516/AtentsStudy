using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonTrigger : MonoBehaviour
{
    /// <summary>
    /// 버튼의 종류 enum
    /// </summary>
    public enum ButtonType
    {
        None,
        Start,  // 게임시작
        Manual, // 조작방법
        Exit,   // 게임종료
        Restart,// 다시하기
        Main,   // 메인으로
    }

    /// <summary>
    /// 이 버튼의 종류
    /// </summary>
    public ButtonType type;

    /// <summary>
    /// 다음 넘어갈 씬의 이름
    /// </summary>
    public string nextSceneName = "Map";

    private void Awake()
    {
        Button button = GetComponentInChildren<Button>();

        switch (type)
        {
            case ButtonType.Start:
                button.onClick.AddListener(OnStart);
                break;
            case ButtonType.Manual:
                button.onClick.AddListener(OnManual);
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

    /// <summary>
    /// 게임 시작을 위한 함수
    /// </summary>
    private void OnStart()
    {
        StartCoroutine(LoadScene(nextSceneName));
    }

    /// <summary>
    /// 메뉴얼 창을 여는 함수
    /// </summary>
    private void OnManual()
    {
        
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
        StartCoroutine(LoadScene(nextSceneName));
    }

    /// <summary>
    /// 처음 씬으로 돌아가는 함수
    /// </summary>
    private void OnMain()
    {
        StartCoroutine(LoadScene(nextSceneName));
    }

    /// <summary>
    /// 씬 넘어가는 코루틴
    /// </summary>
    private IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        // 바로 전환 안되게
        async.allowSceneActivation = false;

        // 로딩이 끝나면
        while (async.progress < 0.9f)
        {
            yield return null;
        }

        // 전환 하기
        async.allowSceneActivation = true;
    }
}
