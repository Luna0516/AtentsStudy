using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 비동기 로딩 관리 매니저 
/// </summary>
public class SceneHandler : Singleton<SceneHandler>
{
    /// <summary>
    /// 로딩 씬 이름
    /// </summary>
    private string loadingSceneName = "LoadingScene";

    /// <summary>
    /// 이전 씬 이름
    /// </summary>
    private string previousSceneName;

    /// <summary>
    /// 이전 씬 이름 설정 및 읽기 (프로퍼티)
    /// </summary>
    public string PreviousSceneName
    {
        get => previousSceneName;
        set
        {
            if (previousSceneName != value)
            {
                previousSceneName = value;
            }
        }
    }

    /// <summary>
    /// 현재 씬 이름
    /// </summary>
    private string presentSceneName;

    /// <summary>
    /// 현재 씬 이름 설정 및 읽기 (프로퍼티)
    /// </summary>
    public string PresentSceneName
    {
        get => presentSceneName;
        set
        {
            if (presentSceneName != value)
            {
                presentSceneName = value;
            }
        }
    }

    /// <summary>
    /// 다음 로딩할 씬 이름
    /// </summary>
    private string nextSceneName;

    /// <summary>
    /// 다음 씬 이름 설정 및 읽기 (프로퍼티)
    /// </summary>
    public string NextSceneName
    {
        get => nextSceneName;
        set
        {
            if(nextSceneName != value)
            {
                nextSceneName = value;

                // 비동기 로딩 시작
                StartCoroutine(LoadScene());
            }
        }
    }

    /// <summary>
    /// 로딩할 다음 씬
    /// </summary>
    AsyncOperation async;

    /// <summary>
    /// 로딩 씬이 검은화면으로 다 뒤덮이면 보낼 신호 (델리게이트)
    /// </summary>
    public System.Action onLoadingSceneCover;

    /// <summary>
    /// 로딩 씬의 검은화면이 다 벗겨지면 보낼 신호 (델리게이트)
    /// </summary>
    public System.Action onLoadingSceneUnCover;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();

        onLoadingSceneCover += StartNextSceneLoading;
        onLoadingSceneUnCover += EndNextSceneLoading;
    }

    private void Start()
    {
        PresentSceneName = SceneManager.GetActiveScene().name;
    }

    private void StartNextSceneLoading()
    {
        PreviousSceneName = PresentSceneName;
        PresentSceneName = NextSceneName;
        nextSceneName = null;

        GameManager.Inst.Init();

        async.allowSceneActivation = true;

        SceneManager.UnloadSceneAsync(PreviousSceneName);
    }

    private void EndNextSceneLoading()
    {
        SceneManager.UnloadSceneAsync(loadingSceneName);
    }

    /// <summary>
    /// 비동기로 로딩씬을 로딩 하는 코루틴
    /// </summary>
    private IEnumerator LoadScene()
    {
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);

        loadingScene.allowSceneActivation = false;

        while (loadingScene.progress < 0.9f)
        {
            yield return null;
        }

        loadingScene.allowSceneActivation = true;

        StartCoroutine(LoadNextScene(NextSceneName));
    }

    /// <summary>
    /// 비동기 씬로딩 하는 코루틴
    /// </summary>
    private IEnumerator LoadNextScene(string nextSceneName)
    {
        async = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);

        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }
    }
}
