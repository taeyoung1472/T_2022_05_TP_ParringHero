using UnityEngine;
[CreateAssetMenu(fileName = "Info", menuName = "Shop")]
public class ShopInfo : ScriptableObject
{
    public Sprite sprite;
    public int price, index;
    public string name, desc;
    public ProductType productType;
    [Header("Æê °ü·Ã")]
    public int MaxHp;
    public int Damage;
    public int TimePetHeal;
    public int TimePerCoin;
}
public enum ProductType
{
    Character,
    Pet,
    ETC
}