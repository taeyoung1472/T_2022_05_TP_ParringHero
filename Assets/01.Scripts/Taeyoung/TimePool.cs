using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePool : PoolAbleObject
{
    [SerializeField] private float time;
    public override void Init_Push()
    {
        print("Push��");
    }

    public override void Init_Pop()
    {
        print("1�ʵڿ� (Push)�� ����");
        StartCoroutine(WaitTimePool());
    }
    IEnumerator WaitTimePool()
    {
        yield return new WaitForSeconds(time);
        PoolManager_Test.instance.Push(poolType, gameObject);
    }
}
