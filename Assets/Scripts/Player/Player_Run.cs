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
        
    }

    public override void FixedExecute()
    {
        playerController.rigid2D.MovePosition(playerController.rigid2D.position + playerController.inputVec * playerController.moveSpeed * Time.fixedDeltaTime);

        if (playerController.inputVec == Vector2.zero)
        {
            playerController.ChangeState(PlayerStates.Idle);
        }
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {

    }
}
