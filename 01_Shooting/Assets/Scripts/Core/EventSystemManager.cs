using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class EventSystemManager : Singleton<EventSystemManager>
{
    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        EnsureEventSystemExists();
    }

    /// <summary>
    /// 이벤트시스템이 싱글톤으로 잘 만들어 졌는지 확인용 함
    /// </summary>
    private void EnsureEventSystemExists()
    {
        EventSystem currentEventSystem = FindObjectOfType<EventSystem>();

        if (currentEventSystem == null)
        {
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystemObj.AddComponent<EventSystem>();
            eventSystemObj.AddComponent<InputSystemUIInputModule>();
            eventSystemObj.AddComponent<EventSystemManager>();
        }
    }
}

