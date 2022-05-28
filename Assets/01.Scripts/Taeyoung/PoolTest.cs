using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    [SerializeField] private PoolType poolType;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PoolManager_Test.instance.Pop(poolType);
        }
    }
}
