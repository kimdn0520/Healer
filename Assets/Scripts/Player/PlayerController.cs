using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates { Idle, Run, Jump, Fall, Attack, die }

public class PlayerController : MonoBehaviour
{
    private Dictionary<PlayerStates, State> states;
    private State currentState;
    public PlayerStates prePlayerState;

    public Rigidbody2D rigid2D;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public float moveSpeed;
    public float jumpForce;
    public bool isGrounded;
    public Vector2 inputVec;
    public float jumpGravityScale;
    public float fallGravityScale;
    public bool isJumping;
    [ReadOnly] public bool isCoyoteTime;
    public float coyoteTimeDuration;
    
    int groundLayerMask;

    [SerializeField] private Vector2 boxCastSize;
    [SerializeField] private float boxCastMaxDistance;
    [SerializeField] private Vector2 boxCenter;

    private void Awake()
    {
        // 변수 초기화
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        isCoyoteTime = false;

        // 컴포넌트 캐싱
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // states에 상태 저장
        states = new Dictionary<PlayerStates, State>();
        states.Add(PlayerStates.Idle, new Player_Idle(this));
        states.Add(PlayerStates.Run, new Player_Run(this));
        states.Add(PlayerStates.Jump, new Player_Jump(this));
        states.Add(PlayerStates.Fall, new Player_Fall(this));

        // 현재 State 설정
        currentState = states[PlayerStates.Idle];
        prePlayerState = PlayerStates.Idle;

        // 최초 실행
        currentState.Enter();
    }

    void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.FixedExecute();
        }
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }

    void LateUpdate()
    {
        if (inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x < 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    public bool CheckGround()
    {
        Vector2 origin = transform.position;

        RaycastHit2D hit = Physics2D.BoxCast(origin - boxCenter, boxCastSize, 0f, Vector2.down, boxCastMaxDistance, groundLayerMask);

        return hit.collider != null;
    }

    void Action_Jump()
    {
        if (isGrounded || (isCoyoteTime && !isGrounded))
        {
            ChangeState(PlayerStates.Jump);
        }
    }

    public void ChangeState(PlayerStates newState)
    {
        // state가 ditionary에 없다면 바뀌지 않습니다.
        if (states[newState] == null) return;

        // 현재 State의 Exit 함수를 한 번 실행합니다.
        currentState.Exit();

        // 현재 State를 바꿔줍니다.
        currentState = states[newState];

        // 현재 State의 Enter 함수를 한 번 실행합니다.
        currentState.Enter();
    }

    // InputSystem을 이용한 inputVec 받는 함수
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Action_Jump();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 origin = transform.position;

        Gizmos.DrawCube(origin - boxCenter, boxCastSize);
    }
}
