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

    }

    public override void FixedExecute()
    {

    }

    public override void Execute()
    {
        if (playerController.inputVec != Vector2.zero)
        {
            playerController.ChangeState(PlayerStates.Walk);
        }
    }

    public override void Exit()
    {

    }
}
