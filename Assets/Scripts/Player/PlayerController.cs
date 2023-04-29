using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates { Idle, Walk, Attack, die }

public class PlayerController : MonoBehaviour
{
    private Dictionary<int, State> states;
    private State currentState;

    public Rigidbody2D rigid2D;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float moveSpeed;
    public Vector2 inputVec;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // states에 상태 저장
        states = new Dictionary<int, State>();
        states.Add((int)PlayerStates.Idle, new Player_Idle(this));
        states.Add((int)PlayerStates.Walk, new Player_Run(this));

        // 현재 State 설정
        currentState = states[(int)PlayerStates.Idle];

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

        animator.SetFloat("Speed", inputVec.magnitude);
    }

    public void ChangeState(PlayerStates newState)
    {
        if (states[(int)newState] == null) return;

        currentState.Exit();

        currentState = states[(int)newState];

        currentState.Enter();
    }

    // InputSystem을 이용한 inputVec 받는 함수
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}
