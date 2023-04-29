using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    /// <summary>
    /// �ش� ���¸� ������ �� 1ȸ ȣ���Ѵ�.
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// �ش� ���¸� ���� ������Ʈ�� �� ȣ��
    /// </summary>
    public abstract void FixedExecute();

    /// <summary>
    /// �ش� ���¸� ������Ʈ�� �� �� ������ ȣ��
    /// </summary>
    public abstract void Execute();

    /// <summary>
    /// �ش� ���¸� ������ �� 1ȸ ȣ��
    /// </summary>
    public abstract void Exit();
}
