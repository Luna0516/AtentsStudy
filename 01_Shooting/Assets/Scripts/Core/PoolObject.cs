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
}