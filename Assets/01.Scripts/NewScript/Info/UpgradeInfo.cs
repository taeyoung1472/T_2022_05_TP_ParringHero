using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Info", menuName = "UpGrade")]
public class UpgradeInfo :ScriptableObject
{
    [Header("정보")]
    public string name;
    public string desc;
    public int price;
    [Header("업그레이드 정보")]
    public UpgradeValue upgrade;
}
[Serializable]
public class UpgradeValue
{

    public bool isRevive = false;
}