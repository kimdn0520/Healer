using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates { Idle, Walk, Jump, Attack, die }

public class PlayerController : MonoBehaviour
{
    private Dictionary<PlayerStates, State> states;
    private State currentState;

    public Rigidbody2D rigid2D;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float moveSpeed;
    public float jumpForce;
    public bool isGrounded;
    public Vector2 inputVec;

    private void Awake()
    {
        // 컴포넌트 캐싱
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // states에 상태 저장
        states = new Dictionary<PlayerStates, State>();
        states.Add(PlayerStates.Idle, new Player_Idle(this));
        states.Add(PlayerStates.Walk, new Player_Run(this));
        states.Add(PlayerStates.Jump, new Player_Jump(this));

        // 현재 State 설정
        currentState = states[PlayerStates.Idle];

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

    // 바닥과의 충돌 감지
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            animator.SetBool("Jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Action_Jump()
    {
        // 점프 가능한 상태인지 확인하고 점프 실행
        if (isGrounded)
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
}
