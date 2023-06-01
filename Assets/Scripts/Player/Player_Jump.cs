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

        playerController.rigid2D.AddForce(new Vector2(0f, playerController.jumpForce), ForceMode2D.Impulse);
    }

    public override void FixedExecute()
    {
        playerController.rigid2D.velocity = new Vector2(playerController.inputVec.x * playerController.moveSpeed, playerController.rigid2D.velocity.y);
    }

    public override void Execute()
    {
        // ���� ������ ����Ǹ� Idle ���·� ��ȯ
        if (!playerController.animator.GetBool("Jump"))
        {
            playerController.ChangeState(PlayerStates.Idle);
        }
    }

    public override void Exit()
    {
        
    }
}
