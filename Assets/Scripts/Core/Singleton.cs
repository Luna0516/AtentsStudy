using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    /// <summary>
    /// 종료 처리에 들어갔는지 확인용 변수
    /// </summary>
    private static bool isShutDown = false;

    /// <summary>
    /// 싱글톤이 처음 만들어 졌는지 확인하는 변수
    /// </summary>
    private static bool initialized = false;

    /// <summary>
    /// 싱글톤 객체
    /// </summary>
    private static T instance;

    /// <summary>
    /// 싱글톤을 읽기 위한 프로퍼티
    /// </summary>
    public static T Inst
    {
        get
        {
            // 종료 처리에 들어간 싱글톤이면 null 반환
            if (isShutDown)
            {
                return null;
            }

            if (!instance)
            {
                T singleton = FindObjectOfType<T>();

                // 만약 찾지 못했으면
                if (!singleton)
                {
                    // 새로 만들기
                    GameObject singletonObj = new GameObject();
                    singletonObj.name = $"{typeof(T).Name} Singleton";
                    singleton = singletonObj.AddComponent<T>();
                }

                instance = singleton;

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            // 첫번째 싱글톤 오브젝트를 캐스팅
            instance = this as T;

            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            // 처음 만든 싱글톤이 아니면
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// 애플리케이션이 종료 되기 전에 실행되는 함수
    /// </summary>
    private void OnApplicationQuit()
    {
        isShutDown = true;
    }

    /// <summary>
    /// 씬이 로드 될때마다 실행할 초기화용 함수 (LoadSceneMode에 따라 다름)
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!initialized)
        {
            OnPreInitialize();
        }

        if (mode != LoadSceneMode.Additive)
        {
            // 싱글로 초기화 되면 실행
            OnInitialize();
        }
    }

    /// <summary>
    /// 싱글톤이 처음 만들어 졌을 때 실행할 초기화용 함수
    /// </summary>
    protected virtual void OnPreInitialize()
    {
        initialized = true;
    }

    /// <summary>
    /// 씬이 Single로 로딩될 때 마다 실행할 초기화용 함수
    /// </summary>
    protected virtual void OnInitialize()
    {

    }
}