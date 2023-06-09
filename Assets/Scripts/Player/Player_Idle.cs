using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Idle : State
{
    private PlayerController playerController;

    public Player_Idle(PlayerController _playerController)
    {
        playerController = _playerController;
    }

    public override void Enter()
    {
        playerController.animator.SetBool("isRunning", false);
    }

    public override void FixedExecute()
    {

    }

    public override void Execute()
    {
        playerController.isGrounded = playerController.CheckGround();

        if (!playerController.isGrounded)
        {
            playerController.ChangeState(PlayerStates.Fall);

            return;
        }

        if (playerController.inputVec != Vector2.zero)
        {
            playerController.ChangeState(PlayerStates.Run);
        }
    }

    public override void Exit()
    {
        playerController.prePlayerState = PlayerStates.Idle;
    }
}
