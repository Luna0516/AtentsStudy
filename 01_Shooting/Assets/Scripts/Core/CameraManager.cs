using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class CameraManager : Singleton<CameraManager>
{
    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        EnsureCameraManager();
    }

    /// <summary>
    /// CameraManager 싱글톤으로 잘 만들어 졌는지 확인용 함
    /// </summary>
    private void EnsureCameraManager()
    {
        CameraManager cameraManager = FindObjectOfType<CameraManager>();

        if (cameraManager == null)
        {
            GameObject eventSystemObj = new GameObject("Main Camera");
            eventSystemObj.AddComponent<Camera>();
            eventSystemObj.AddComponent<AudioListener>();
            eventSystemObj.AddComponent<CameraManager>();
        }
    }
}
