using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionPlant : Reposition
{
    // 컴포넌트
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        MoveInit();
    }

    protected override void MoveInit()
    {
        spriteRenderer.flipY = Random.Range(0, 2) > 0;

        if (spriteRenderer.flipY)
        {
            transform.position = new Vector3(transform.position.x, Random.Range(1, 4));
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Random.Range(-1, -4));
        }
    }
}
