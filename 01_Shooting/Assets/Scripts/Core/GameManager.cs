using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 플레이어
    /// </summary>
    private Player player;

    /// <summary>
    /// 플레이어 참조용 프로퍼티
    /// </summary>
    public Player Player
    {
        get
        {
            // 없으면 현재 씬에서 찾아서 반환
            if (!player)
            {
                player = FindObjectOfType<Player>();
            }

            return player;
        }
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        player = FindObjectOfType<Player>();
    }
}
