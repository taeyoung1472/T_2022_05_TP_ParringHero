using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolAbleObject : MonoBehaviour
{
    protected PoolType type;
    public PoolType poolType { get { return type; } set { type = value; } }
    /// <summary>
    /// �����ö� �ʱ�ȣ
    /// </summary>
    public abstract void Init_Pop();
    /// <summary>
    /// ������ �ʱ�ȭ
    /// </summary>
    public abstract void Init_Push();
}
