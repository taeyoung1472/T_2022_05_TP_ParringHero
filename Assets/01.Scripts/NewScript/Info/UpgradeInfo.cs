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
    [Header("수치 변화")]
    public int timePerCoin = 0;
    public int timePerHp = 0;
    [Header("극적 변화")]
    public bool isRevive = false;
    [Space(10)]
    public WeaponType weaponType = WeaponType.None;
    [Space(10)]
    public SkillType skillType = SkillType.None;
    [Space(10)]
    public UtilityType utilityType = UtilityType.None;
}
public enum WeaponType
{
    None,
    Sword,
    Length,
    Shild
}
public enum SkillType
{
    None,
    Meteo,
    Run
}
public enum UtilityType
{
    None,
    Multi,
    PerpectKill
}