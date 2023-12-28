using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utile
{
    /// <summary>
    /// 셔플 함수
    /// </summary>
    /// <typeparam name="T">배열의 타입</typeparam>
    /// <param name="source">셔플할 배열</param>
    public static void Shuffle<T>(T[] source)
    {
        for (int i = source.Length - 1; i > -1; i--)
        {
            int index = Random.Range(0, i);
            (source[index], source[i]) = (source[i], source[index]);
        }
    }
}
