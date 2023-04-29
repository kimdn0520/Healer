using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class ZombieRandomMove : ActionNode
{
    private float speed = 10f;
    private float minMoveDistance = 1f;
    private float maxMoveDistance = 3f;
    Vector2 randomPosition;
    Vector2 moveDir;

    protected override void OnStart()
    {
        randomPosition = (Vector2)context.rigid2D.position + Random.insideUnitCircle.normalized * Random.Range(minMoveDistance, maxMoveDistance);

        moveDir = randomPosition - context.rigid2D.position;

        moveDir.Normalize();
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        context.rigid2D.MovePosition(context.rigid2D.position + moveDir * speed * Time.deltaTime);
        context.rigid2D.velocity = Vector2.zero;

        if (Vector2.Distance(context.rigid2D.position, randomPosition) < 0.1f)
        {
            return State.Success;
        }
        else
        {
            return State.Running;
        }
    }

    public override void OnDrawGizmos()
    {

    }
}
