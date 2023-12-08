using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public System.Action onDisable;

    protected virtual void OnDisable()
    {
        onDisable?.Invoke();
    }

    /// <summary>
    /// 오브젝트의 지속시간 후 비활성화 시키는 코루틴
    /// </summary>
    /// <param name="lifeTime">설정할 지속시간</param>
    protected IEnumerator LifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}