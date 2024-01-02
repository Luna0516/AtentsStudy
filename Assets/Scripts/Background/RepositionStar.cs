using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionStar : Reposition
{
    // 컴포넌트
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void MoveInit()
    {
        // C#에서 숫자 앞에 0b_를 붙이면 이진수라는 의미 (0b_0100_1010), 10진수 74
        // C#에서 숫자 앞에 0x를 붙이면 16진수라는 의미 (0x4a), 10진수 74
        int rand = Random.Range(0, 4); // 0(0b_00),1(0b_01),2(0b_10),3(0b_11)

        // rand의 첫번째 비트가 0이냐 1이냐, true면 첫번째 비트는 1, false면 첫번째 비트는 0
        spriteRenderer.flipX = ((rand & 0b_01) != 0);
        // rand의 두번째 비트가 0이냐 1이냐, true면 두번째 비트는 1, false면 두번째 비트는 0
        spriteRenderer.flipY = ((rand & 0b_10) != 0);
    }
}
