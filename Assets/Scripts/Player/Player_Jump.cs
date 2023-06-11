using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : State
{
    private PlayerController playerController;

    public Player_Jump(PlayerController _playerController)
    {
        playerController = _playerController;
    }

    public override void Enter()
    {
        playerController.animator.SetBool("Jump", true);

        playerController.rigid2D.gravityScale = playerController.jumpGravityScale;

        playerController.rigid2D.AddForce(new Vector2(0f, playerController.jumpForce), ForceMode2D.Impulse);
    }

    public override void FixedExecute()
    {
        playerController.rigid2D.velocity = new Vector2(playerController.inputVec.x * playerController.moveSpeed, playerController.rigid2D.velocity.y);
    }

    public override void Execute()
    {
        if(playerController.rigid2D.velocity.y <= 0)
        {
            playerController.ChangeState(PlayerStates.Fall);
        }
    }

    public override void Exit()
    {
        playerController.animator.SetBool("Jump", false);
    }
}
