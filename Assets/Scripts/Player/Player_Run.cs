using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Run : State
{
    private PlayerController playerController;

    public Player_Run(PlayerController _playerController)
    {
        playerController = _playerController;
    }

    public override void Enter()
    {
        playerController.animator.SetBool("isRunning", true);
    }

    public override void FixedExecute()
    {
        playerController.rigid2D.velocity = new Vector2(playerController.inputVec.x * playerController.moveSpeed, playerController.rigid2D.velocity.y);
    }

    public override void Execute()
    {
        playerController.isGrounded = playerController.CheckGround();

        if (!playerController.isGrounded)
        {
            playerController.ChangeState(PlayerStates.Fall);

            return;
        }

        if (playerController.inputVec == Vector2.zero)
        {
            playerController.ChangeState(PlayerStates.Idle);
        }
    }

    public override void Exit()
    {
        // 빠져나갈때는 속도를 zero 해주면 미끄러지지 않는다.
        playerController.rigid2D.velocity = Vector2.zero;

        playerController.prePlayerState = PlayerStates.Run;
    }
}
