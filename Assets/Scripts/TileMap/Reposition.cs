using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���Ѹ� �̵��� ���� ���ġ ��ũ��Ʈ
/// </summary>
public class Reposition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        // �÷��̾�� TileMap�� x, y ���� ���
        Vector3 playerPos = GameManager.Instance.playerController.transform.position;
        Vector3 groundPos = transform.position;

        switch (transform.tag)
        {
            case "Ground":
                float diffX = playerPos.x - groundPos.x;
                float diffY = playerPos.y - groundPos.y;
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 60);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 60);
                }
                break;
            case "Enemy":
                break;
        }
    }
}
