using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonScene : MonoBehaviour
{
    private Button button;

    public string nextSceneName;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(LoadingStart);
    }

    private void LoadingStart()
    {
        SceneHandler.Inst.NextSceneName = nextSceneName;
    }
}
