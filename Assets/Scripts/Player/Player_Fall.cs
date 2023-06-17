using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fall : State
{
    private PlayerController playerController;
  
    private float coyoteTimeElapsed = 0f;

    public Player_Fall(PlayerController _playerController)
    {
        playerController = _playerController;
    }

    public override void Enter()
    {
        playerController.animator.SetBool("Fall", true);

        playerController.rigid2D.gravityScale = playerController.fallGravityScale;

        // ���� PlayerStates�� Jump ���°� �ƴϾ��� ��쿡�� �ڿ���Ÿ���� ������ ��
        if (playerController.prePlayerState != PlayerStates.Jump)
        {
            playerController.isCoyoteTime = true;

            coyoteTimeElapsed = 0f;
        }
    }

    public override void FixedExecute()
    {
        playerController.rigid2D.velocity = new Vector2(playerController.inputVec.x * playerController.moveSpeed, playerController.rigid2D.velocity.y);
    }

    public override void Execute()
    {
        playerController.isGrounded = playerController.CheckGround();

        if (playerController.isGrounded)
        {
            playerController.ChangeState(PlayerStates.Idle);
        }

        // �ڿ��� Ÿ���� ����� ���, ��� �ð��� ����ϰ� �ð��� �ʰ��ϸ� �ڿ��� Ÿ���� ����
        if (playerController.isCoyoteTime)
        {
            coyoteTimeElapsed += Time.deltaTime;

            if (coyoteTimeElapsed >= playerController.coyoteTimeDuration)
            {
                playerController.isCoyoteTime = false;
            }
        }
    }

    public override void Exit()
    {
        playerController.animator.SetBool("Fall", false);

        playerController.rigid2D.gravityScale = playerController.jumpGravityScale;

        playerController.isCoyoteTime = false;

        playerController.prePlayerState = PlayerStates.Fall;
    }
}
