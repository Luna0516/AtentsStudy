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
    /// 부스트 쓸때 이동 속도
    /// </summary>
    private float boostSpeed;

    /// <summary>
    /// 초기 이동 속도
    /// </summary>
    private float defaultSpeed;

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

    /// <summary>
    /// 총알 발사 위치
    /// </summary>
    private Transform firePos;

    /// <summary>
    /// 총알 발사 코루틴
    /// </summary>
    private IEnumerator fireCorou;

    // 컴포넌트
    Animator anim;

    // 인풋 시스템
    PlayerInputActions inputActions;

    private void Awake()
    {
        fireCorou = FireCoroutine();

        anim = GetComponent<Animator>();

        inputActions = new PlayerInputActions();

        // 이동속도 저장
        boostSpeed = moveSpeed * 2f;
        defaultSpeed = moveSpeed;

        // 총알 발사 위치 저장
        firePos = transform.GetChild(0);
    }

    private void Start()
    {
        elapsedTime = fireDelay;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Fire.performed += StartFire;
        inputActions.Player.Fire.canceled += StoptFire;
        inputActions.Player.Boost.performed += OnBoost;
        inputActions.Player.Boost.canceled += OnBoost;
    }

    private void OnDisable()
    {
        inputActions.Player.Boost.canceled -= OnBoost;
        inputActions.Player.Boost.performed -= OnBoost;
        inputActions.Player.Fire.canceled -= StoptFire;
        inputActions.Player.Fire.performed -= StartFire;
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

    /// <summary>
    /// 총알 발사 버튼을 누르면 실행할 함수
    /// </summary>
    private void StartFire(InputAction.CallbackContext context)
    {
        StartCoroutine(fireCorou);
    }

    /// <summary>
    /// 총알 발사 버튼을 떼면 실행할 함수
    /// </summary>
    private void StoptFire(InputAction.CallbackContext context)
    {
        StopCoroutine(fireCorou);
    }

    /// <summary>
    /// 총알 발사 코루틴
    /// </summary>
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            if(elapsedTime > fireDelay)
            {
                Factory.Inst.GetObject(PoolObjectType.PlayerBullet, firePos.position);
                elapsedTime = 0;
            }

            yield return null;
        }
    }

    private void OnBoost(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            moveSpeed = defaultSpeed;
        }
        else
        {
            moveSpeed = boostSpeed;
        }
    }
}
