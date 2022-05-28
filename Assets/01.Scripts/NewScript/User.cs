[System.Serializable]
public class User
{
    public int coin;

    public int fixedMaxHP;
    public int fixedDamage;
    public int timePerHealHp;
    public int timePerCoin;
    public int reberseChance;

    public int petMaxHp;
    public int petDamage;
    public int petTimePetHeal;
    public int petTimePerCoin;

    public int playerIndex;
    public int petIndex;

    //public bool isTutoClear;

    public bool[] isUnlockUpgrade;
    public bool[] isUnlockCharacter;
    public bool[] isUnlockPet;
}