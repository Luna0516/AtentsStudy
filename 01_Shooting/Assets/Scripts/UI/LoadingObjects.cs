using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingObjects : MonoBehaviour
{
    /// <summary>
    /// 로딩 할때 불러올 큐브들 => 2차원 배열
    /// </summary>
    private GameObject[,] loadingCubes;

    /// <summary>
    /// 다음 로딩 큐브가 나오는데 걸리는 시간
    /// </summary>
    public float waitTime;

    /// <summary>
    /// 다음 로딩 큐브가 나오는데 걸리는 시간
    /// </summary>
    private WaitForSeconds wait;
    int y;
    int x;
    
    private void Awake()
    {
        // 첫 자식 오브젝트의 개수 => 세로로 채워져 있음
        y = transform.childCount;

        // 자식의 오브젝트의 자식 오브젝트 개수 => 가로로 채워져 있음
        x = transform.GetChild(0).childCount;

        // 배열 크기
        loadingCubes = new GameObject[y, x];

        // 원점은 (왼쪽상단 시작 -> 오른쪽하단 종료)

        // 배열 초기화
        for(int i = 0; i < y; i++)
        {
            Transform child = transform.GetChild(i);

            for(int j = 0; j < x; j++)
            {
                // 배열에 넣고
                loadingCubes[i, j] = child.GetChild(j).gameObject;

                // 비활성화하기
                loadingCubes[i, j].SetActive(false);
            }
        }

        // 대기시간 설정
        wait = new WaitForSeconds(waitTime);
    }

    private void Start()
    {
        StartCoroutine(ActiveLoadingLine(true));
    }

    int yCount = 1;

    private IEnumerator ActiveLoadingLine(bool active)
    {
        for (int i = 0; i < y; i++)
        {
            StartCoroutine(ActiveLoadingCube(i, active));
            yield return wait;
        }
    }

    /// <summary>
    /// 로딩 큐브다 다 켜졌는지 확인하는 변수
    /// </summary>
    bool isLoading = false;

    private IEnumerator ActiveLoadingCube(int _y, bool active)
    {
        for(int i = 0; i < x; i++)
        {
            loadingCubes[_y, i].SetActive(active);

            yield return wait;
        }

        yCount++;

        if (yCount == y)
        {
            isLoading = active;
            yCount = 0;

            if (isLoading)
            {
                SceneHandler.Inst.onLoadingSceneCover?.Invoke();
                StartCoroutine(ActiveLoadingLine(false));
            }
            else
            {
                SceneHandler.Inst.onLoadingSceneUnCover?.Invoke();
            }
        }
    }
}
