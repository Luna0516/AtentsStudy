using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : PoolObject
{
    /// <summary>
    /// 풀에서 관리한 게임오브젝트 프리펩
    /// </summary>
    [Header("풀 프리펩")]
    public GameObject prefab;

    /// <summary>
    /// 풀의 크기
    /// </summary>
    [Header("풀 크기 (2^n 으로 적을 것!)")]
    public int poolSize;

    /// <summary>
    /// 생성한 오브젝트의 배열
    /// </summary>
    private T[] pool;

    /// <summary>
    /// 풀에서 꺼내쓸수 있는 게임 오브젝트가 들어있는 큐
    /// </summary>
    Queue<T> poolQueue;

    /// <summary>
    /// 풀매니저 생성시 초기화용 함수
    /// </summary>
    public void Initialize()
    {
        if(pool == null)
        {
            pool = new T[poolSize];
            poolQueue = new Queue<T>(poolSize);

            GenerateObjects(0, poolSize, pool);
        }
        else
        {
            foreach(T obj in pool)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 풀에서 오브젝트를 하나 꺼낸 후 돌려주는 함수
    /// </summary>
    /// <param name="spawnTransform">오브젝트 꺼낼 때 설정할 위치와 회전과 스케일</param>
    /// <returns>큐에서 꺼내고 "활성화"시킨 오브젝트</returns>
    public T GetObject(Transform spawnTransform = null)
    {
        // 큐에 사용할 오브젝트가 있으면
        if (poolQueue.Count > 0)
        {
            T comp = poolQueue.Dequeue();

            // 미리 설정할 트랜스폼이 있으면 적용
            if (spawnTransform != null)          
            {
                comp.transform.SetPositionAndRotation(spawnTransform.position, spawnTransform.rotation);
                comp.transform.localScale = spawnTransform.localScale;
            }
            else
            {
                comp.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                comp.transform.localScale = Vector3.one;
            }

            comp.gameObject.SetActive(true);
            return comp;
        }
        else
        {
            // ExpandPool(); 풀 확장 함수
            return GetObject(spawnTransform);
        }
    }

    /// <summary>
    /// 풀을 두배로 확장시키는 함수
    /// </summary>
    private void ExpandPool()
    {
        Debug.LogWarning($"{gameObject.name} 풀 사이즈 증가. {poolSize} -> {poolSize * 2}");

        int newSize = poolSize * 2;
        T[] newPool = new T[newSize];

        // 이전 풀 배열을 새로운 풀 배열에 저장
        for (int i = 0; i < poolSize; i++)
        {
            newPool[i] = pool[i];
        }

        GenerateObjects(poolSize, newSize, newPool);

        pool = newPool;
        poolSize = newSize;
    }

    /// <summary>
    /// 오브젝트 생성해서 배열에 추가해주는 함수
    /// </summary>
    /// <param name="start">배열의 시작 인덱스</param>
    /// <param name="end">배열의 마지막 인덱스-1</param>
    /// <param name="newArray">생성된 오브젝트가 들어갈 배열</param>
    private void GenerateObjects(int start, int end, T[] newArray)
    {
        for (int i = start; i < end; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.name = $"{prefab.name}_{i}";

            T comp = obj.GetComponent<T>();

            // PoolObject가 비활성화 될 때 래디큐로 되돌리기
            comp.onDisable += () => poolQueue.Enqueue(comp);
            
            newArray[i] = comp;

            obj.SetActive(false);
        }
    }
}
