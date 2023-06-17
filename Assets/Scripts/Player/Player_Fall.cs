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

        // 이전 PlayerStates가 Jump 상태가 아니었을 경우에만 코요테타임을 적용할 것
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

        // 코요테 타임이 적용된 경우, 경과 시간을 계산하고 시간이 초과하면 코요테 타임을 종료
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
