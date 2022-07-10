using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] private SpawnInfo[] spawnInfos;
    [SerializeField] private float spawnTime;
    [SerializeField] private int bossSpawnLateCount;
    [SerializeField] private GameObject boss;
    int spawnCount = 0;
    bool isBossAlive;
    bool isSpawned;
    public bool IsBossAlive { set { isBossAlive = value; } }

    void Start()
    {
        StartCoroutine(SpawnCor());
    }
    private IEnumerator SpawnCor()
    {
        int temp = 0;
        int total = 0;
        while (true)
        {
            temp = 0;
            total = 0;
            yield return new WaitForSeconds(spawnTime * UnityEngine.Random.Range(0.5f, 1.5f));
            temp = UnityEngine.Random.Range(0,100);
            for (int i = 0; i < spawnInfos.Length; i++)
            {
                total += spawnInfos[i].SpawnPercent;
                if (temp < total)
                {
                    if(spawnCount >= bossSpawnLateCount && !isBossAlive && !isSpawned)
                    {
                        boss.SetActive(true);
                        isBossAlive = true;
                        isSpawned = true;
                        yield return new WaitUntil(() => !isBossAlive);
                    }
                    else
                    {
                        GameObject obj = PoolManager_Test.instance.Pop(spawnInfos[i].PoolType);
                        obj.transform.position = spawnPos.position;
                        spawnCount++;
                    }
                    break;
                }
            }
        }
    }
}
[Serializable]
public class SpawnInfo
{
    public PoolType PoolType { get { return poolType; } }
    public int SpawnPercent { get { return spawnPercent; } }
    [SerializeField] private PoolType poolType;
    [SerializeField] private int spawnPercent;
}