using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates { Idle, Walk, Jump, Fall, Attack, die }

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
    public float jumpGravityScale;
    public float fallGravityScale;
    int groundLayerMask;

    [SerializeField] float raycastGroundDistance;

    private void Awake()
    {
        // ���� �ʱ�ȭ
        raycastGroundDistance = 0.7f;
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");

        // ������Ʈ ĳ��
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // states�� ���� ����
        states = new Dictionary<PlayerStates, State>();
        states.Add(PlayerStates.Idle, new Player_Idle(this));
        states.Add(PlayerStates.Walk, new Player_Run(this));
        states.Add(PlayerStates.Jump, new Player_Jump(this));
        states.Add(PlayerStates.Fall, new Player_Fall(this));

        // ���� State ����
        currentState = states[PlayerStates.Idle];

        // ���� ����
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastGroundDistance, groundLayerMask);

        return hit.collider != null;
    }

    void Action_Jump()
    {
        // ���� ������ �������� Ȯ���ϰ� ���� ����
        isGrounded = CheckGround();

        if (CheckGround())
        {
            ChangeState(PlayerStates.Jump);
        }
    }

    public void ChangeState(PlayerStates newState)
    {
        // state�� ditionary�� ���ٸ� �ٲ��� �ʽ��ϴ�.
        if (states[newState] == null) return;

        // ���� State�� Exit �Լ��� �� �� �����մϴ�.
        currentState.Exit();

        // ���� State�� �ٲ��ݴϴ�.
        currentState = states[newState];

        // ���� State�� Enter �Լ��� �� �� �����մϴ�.
        currentState.Enter();
    }

    // InputSystem�� �̿��� inputVec �޴� �Լ�
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
        Gizmos.DrawRay(transform.position, Vector2.down * raycastGroundDistance);
    }
}
