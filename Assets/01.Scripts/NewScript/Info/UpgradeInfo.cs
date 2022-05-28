using UnityEngine;
[CreateAssetMenu(fileName = "Info", menuName = "UpGrade")]
public class UpgradeInfo :ScriptableObject
{
    public Sprite sprite;
    public int price;
    public string name;
    public string desc;

    public int index;
    public int hp;
    public int damage;
    public int timePerHealHp;
    public int timePerGetCoin;
    public int time;
    public float coin;
    public bool isWhile;
    public bool isReberse;
}