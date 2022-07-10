using System.Collections;
using System.Collections.Generic;
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
    public WeaponType weaponType;
    public List<SkillType> skillTypes;

    public List<DictionaryJson<string, bool>> upgradeBoolDic;
    public bool CheckJsonDicContains(string key)
    {
        foreach (DictionaryJson<string, bool> dic in upgradeBoolDic)
        {
            if (dic.Key == key)
                return true;
        }
        return false;
    }
    public int MatchKeyIndex(string key)
    {
        int i = 0;
        foreach (DictionaryJson<string, bool> dic in upgradeBoolDic)
        {
            if (dic.Key == key)
                return i;
            i++;
        }
        return -1;
    }
    //public bool[] isUnlockUpgrade;
    public bool[] isUnlockCharacter;
    public bool[] isUnlockPet;
}