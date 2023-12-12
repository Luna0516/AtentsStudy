using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Player player;

    public Player Player
    {
        get
        {
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
