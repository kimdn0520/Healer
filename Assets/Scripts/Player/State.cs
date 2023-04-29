using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    /// <summary>
    /// 해당 상태를 시작할 때 1회 호출한다.
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// 해당 상태를 물리 업데이트할 때 호출
    /// </summary>
    public abstract void FixedExecute();

    /// <summary>
    /// 해당 상태를 업데이트할 때 매 프레임 호출
    /// </summary>
    public abstract void Execute();

    /// <summary>
    /// 해당 상태를 종료할 때 1회 호출
    /// </summary>
    public abstract void Exit();
}
