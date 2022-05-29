using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Info", menuName = "UpGrade")]
public class UpgradeInfo :ScriptableObject
{
    [Header("����")]
    public string name;
    public string desc;
    public int price;
    [Header("���׷��̵� ����")]
    public UpgradeValue upgrade;
}
[Serializable]
public class UpgradeValue
{

    public bool isRevive = false;
}