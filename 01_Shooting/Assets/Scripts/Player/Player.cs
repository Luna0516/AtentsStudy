using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    [Header("이동 속도")]
    public float moveSpeed;

    /// <summary>
    /// 총알 발사 딜레이 시간
    /// </summary>
    [Header("총알 발사 속도")]
    public float fireDelay;

    /// <summary>
    /// 총알 발사 딜레이 확인용 시간
    /// </summary>
    private float elapsedTime;

    /// <summary>
    /// 애니메이터 파라메터 InputY
    /// </summary>
    readonly int Hash_InputY = Animator.StringToHash("InputY");

    /// <summary>
    /// 방향키로 입력받은 벡터값
    /// </summary>
    private Vector2 inputVec;

    // 컴포넌트
    Animator anim;
    Rigidbody2D rigid;

    // 인풋 시스템
    PlayerInputActions inputActions;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * inputVec * moveSpeed);
    }

    /// <summary>
    /// 이동 입력값 가져오는 함수
    /// </summary>
    /// <param name="context">이동 입력값(Vector2)</param>
    private void OnMove(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();

        anim.SetFloat(Hash_InputY, inputVec.y);
    }

}
