using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 플레이어의 목숨
    /// </summary>
    private int life;

    /// <summary>
    /// 플레이어의 목숨에 변화 설정을 위한 프로퍼티
    /// </summary>
    public int Life
    {
        get => life;
        private set
        {
            // 목숨이 변경하는 값과 다르면
            if (life != value && value > -1)
            {
                life = value;

                onLifeCountChange?.Invoke(life);

                if(life > 0)
                {
                    // 무적 시간 코루틴 실행
                    StartCoroutine(InvisibleCoroutine());
                }
                else
                {
                    // 목숨이 0 이하면 죽는 함수 실행
                    Die();
                }
            }
        }
    }

    /// <summary>
    /// 기본 체력 (불러오기용)
    /// </summary>
    private int defaultLife = 3;

    /// <summary>
    /// 무적 시간
    /// </summary>
    private float invincibilityDuration = 2f;

    /// <summary>
    /// 깜박임 시간
    /// </summary>
    private float blinkInterval = 0.1f;

    /// <summary>
    /// 깜박임 시간 코루틴용
    /// </summary>
    private WaitForSeconds blinkTimer;

    /// <summary>
    /// 이동 속도
    /// </summary>
    [Header("이동 속도")]
    public float moveSpeed = 3;

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
    private float fireDelay = 0.2f;

    /// <summary>
    /// 총알 발사 딜레이 시간 설정용 프로퍼티
    /// </summary>
    public float FireDelay
    {
        get => fireDelay;
        private set
        {
            if(fireDelay != value)
            {
                // 발사 속도 최소값으로 막기
                fireDelay = Mathf.Max(minFireDelay, value);
            }
        }
    }

    private float minFireDelay;

    /// <summary>
    /// 초기 발사 딜레이 시간
    /// </summary>
    private float defaultfireDelay;

    /// <summary>
    /// 총알 발사 딜레이 확인용 시간
    /// </summary>
    private float elapsedTime;

    /// <summary>
    /// 애니메이터 파라메터 InputY
    /// </summary>
    readonly int Hash_InputY = Animator.StringToHash("InputY");

    /// <summary>
    /// 플레이어의 목숨이 변할 때마다 보낼 델리게이트 신호
    /// </summary>
    public System.Action<int> onLifeCountChange;

    /// <summary>
    /// 플레이어가 죽으면 보낼 델리게이트 신호
    /// </summary>
    public System.Action onDie;

    /// <summary>
    /// 방향키로 입력받은 벡터값
    /// </summary>
    private Vector2 inputVec;

    /// <summary>
    /// 총알 발사 코루틴
    /// </summary>
    private IEnumerator fireCorou;

    /// <summary>
    /// 총알 발사 위치
    /// </summary>
    private Transform firePos;

    /// <summary>
    /// 총알 발사 이펙트
    /// </summary>
    private GameObject fireEffect;

    /// <summary>
    /// 총알 발사 이펙트 보이는 시간
    /// </summary>
    private WaitForSeconds fireEffectWait = new WaitForSeconds(0.01f);

    // 컴포넌트
    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    // 인풋 시스템
    PlayerInputActions inputActions;

    private void Awake()
    {
        fireCorou = FireCoroutine();

        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        inputActions = new PlayerInputActions();

        // 이동속도 저장
        boostSpeed = moveSpeed * 2f;
        defaultSpeed = moveSpeed;

        // 발사 속도 저장
        defaultfireDelay = fireDelay;

        // 최고 발사 속도 지정
        minFireDelay = defaultfireDelay * 0.5f;

        // 코루틴용 대기 시간 저장
        blinkTimer = new WaitForSeconds(blinkInterval);

        // 총알 발사 및 이펙트 저장
        firePos = transform.GetChild(0);
        fireEffect = transform.GetChild(1).gameObject;
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

        // 초기화
        Life = defaultLife;
        GameManager.Inst.Score = 0;
        FireDelay = defaultfireDelay;
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

        // 델리게이트 연결 해제
        onLifeCountChange = null;
        onDie = null;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * inputVec * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.layer == LayerMask.NameToLayer("InvincibleLayer")) return;

        // 충돌한 게임오브젝트의 태그가 Enemy or EnemyBullet이면 목숨 감소;
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            Life--;
        }

        // 충돌한 오브젝트가 Item 인지 확인
        if (collision.gameObject.CompareTag("Item"))
        {
            ItemBase item = collision.gameObject.GetComponent<ItemBase>();

            if(item != null)
            {
                SetItemApply(item);
            }

            collision.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 플레이어가 죽으면 실행될 함수
    /// </summary>
    private void Die()
    {
        Factory.Inst.GetObject(PoolObjectType.ExplosionEffect, transform.position);
        if (GameManager.Inst != null)
        {
            GameManager.Inst.onGameEnd?.Invoke(false);
            GameManager.Inst.GameState = GameState.End;
        }
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 총알 발사 코루틴
    /// </summary>
    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            if(elapsedTime > fireDelay)
            {
                StartCoroutine(FireEffect());
                Factory.Inst.GetObject(PoolObjectType.PlayerBullet, firePos.position);
                elapsedTime = 0;
            }

            yield return null;
        }
    }

    /// <summary>
    /// 총알 발사 이펙트 코루틴
    /// </summary>
    private IEnumerator FireEffect()
    {
        fireEffect.SetActive(true);
        yield return fireEffectWait;
        fireEffect.SetActive(false);
    }

    /// <summary>
    /// 무적 상태 로직용 코루틴 (깜박임 / 충돌 안됨)
    /// </summary>
    private IEnumerator InvisibleCoroutine()
    {
        gameObject.layer = LayerMask.NameToLayer("InvincibleLayer");

        float invisibleTime = 0;

        while (invisibleTime < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return blinkTimer;

            invisibleTime += blinkInterval;
        }

        gameObject.layer = LayerMask.NameToLayer("Player"); ;

        spriteRenderer.enabled = true;
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
    /// 부스트 버튼의 상호작용에 따른 이동속도 변화 함수
    /// </summary>
    private void OnBoost(InputAction.CallbackContext context)
    {
        // 부스트 버튼을 누를 때 속도 상승 떼면 속도 원래대로
        if(context.canceled)
        {
            moveSpeed = defaultSpeed;
        }
        else
        {
            moveSpeed = boostSpeed;
        }
    }

    /// <summary>
    /// 아이템 능력치 적용 함수
    /// </summary>
    /// <param name="item">얻은 아이템</param>
    private void SetItemApply(ItemBase item)
    {
        // 아이템 타입 가져오기
        ItemType itemType = item.Type;

        // 아이템 타입 별로 인터페이스 확인 하고 능력치 적용
        switch (itemType)
        {
            case ItemType.PowerUp:
                IPower power = item as IPower;
                FireDelay -= power.PowerUpValue;
                break;
            case ItemType.None:
            default:
                break;
        }
    }
}
