using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fall : State
{
    private PlayerController playerController;

    public Player_Fall(PlayerController _playerController)
    {
        playerController = _playerController;
    }

    public override void Enter()
    {
        playerController.animator.SetBool("Fall", true);

        playerController.rigid2D.gravityScale = playerController.fallGravityScale;
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
    }

    public override void Exit()
    {
        playerController.animator.SetBool("Fall", false);

        playerController.rigid2D.gravityScale = playerController.jumpGravityScale;
    }
}
