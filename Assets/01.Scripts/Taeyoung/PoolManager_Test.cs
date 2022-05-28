using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PoolManager_Test : MonoBehaviour
{
    public static PoolManager_Test instance;//�̱���
    [SerializeField] private PoolData[] poolAbleDatas;//Ǯ�� Ÿ�� ������ŭ �����
    [SerializeField] private LocalPoolManager localTamplate;//LocalPoolManager ���ø�
    Dictionary<PoolType, LocalPoolManager> localPoolDic = new Dictionary<PoolType, LocalPoolManager>();//PoolType���� �˻��ϱ� ���� ��ųʸ�
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        for (int i = 0; i < poolAbleDatas.Length; i++)
        {
            LocalPoolManager localPool = Instantiate(localTamplate, transform);
            PoolData data = poolAbleDatas[i];
            localPool.Init(data.InitCount, data.PoolAbleObject, data.PoolType);
            localPoolDic.Add(data.PoolType, localPool);
            localPool.name = $"{localPool.name} : {data.PoolType}";
        }
    }
    /// <summary>
    /// Type�� �´� ������Ʈ ��������
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject Pop(PoolType type)
    {
        return localPoolDic[type].Pop().gameObject;
    }
    /// <summary>
    /// Type�� �°� ������Ʈ �ֱ�
    /// </summary>
    /// <param name="type"></param>
    /// <param name="obj"></param>
    public void Push(PoolType type, GameObject obj)
    {
        localPoolDic[type].Push(obj.GetComponent<PoolAbleObject>());
    }
}
[Serializable]
public class PoolData/*Ǯ�� ������*/
{
    [SerializeField] private PoolType poolType;
    [SerializeField] private int initCount;
    [SerializeField] private PoolAbleObject poolAbleObject;
    public PoolType PoolType { get { return poolType; } }
    public int InitCount { get { return initCount; } }
    public PoolAbleObject PoolAbleObject { get { return poolAbleObject; } }
}
public enum PoolType/*Ǯ�� Ÿ��(Ǯ���� ������ �������� ��� �߰�*/
{
    Enemy_Soul,
    Enemy_Horse,
    Enemy_Gobline,
    Sound,
    Effect,
    ETC
}