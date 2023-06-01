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
        playerController.rigid2D.velocity = playerController.inputVec * playerController.moveSpeed;
    }

    public override void Execute()
    {
        if (playerController.inputVec == Vector2.zero)
        {
            playerController.ChangeState(PlayerStates.Idle);
        }
    }

    public override void Exit()
    {
        // ������������ �ӵ��� zero ���ָ� �̲������� �ʴ´�.
        playerController.rigid2D.velocity = Vector2.zero;
    }
}
