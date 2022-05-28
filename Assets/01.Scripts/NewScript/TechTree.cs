using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class TechTree : MonoBehaviour
{
    public TechStateEnum State { get { return state; } set { state = value; } }
    [SerializeField] private UpgradeInfo upgradeInfo;
    [SerializeField] protected TechTree[] upTierTechs;
    [SerializeField] protected TechTree[] downTierTechs;
    [SerializeField] protected TechTree[] cantChoiceWithTechs;
    [SerializeField] protected TechStateEnum state = TechStateEnum.Locked;
    Image image;
    public void Start()
    {
        image = GetComponent<Image>();
        if (GameManager.Instance.currentUser.isUnlockUpgrade[upgradeInfo.index] && state == TechStateEnum.First)
        {
            Purchase();
            return;
        }
        switch (state)
        {
            case TechStateEnum.First:
                state = TechStateEnum.PurchaseEnabled;
                image.color = Color.gray;
                Check();
                break;
            case TechStateEnum.Locked:
                image.color = Color.black;
                break;
            case TechStateEnum.PurchaseEnabled:
                image.color = Color.gray;
                break;
            case TechStateEnum.UnLcoked:
                image.color = Color.white;
                break;
        }
    }
    public void Purchase()
    {
        if(state == TechStateEnum.PurchaseEnabled || state == TechStateEnum.First || GameManager.Instance.currentUser.isUnlockUpgrade[upgradeInfo.index])
        {
            state = TechStateEnum.UnLcoked;
            image = GetComponent<Image>();
            image.color = Color.white;
            for (int i = 0; i < downTierTechs.Length; i++)
            {
                downTierTechs[i].Check();
            }
            print($"{gameObject.name} ÀÌ Purchase() µÊ");
        }
        else
        {
            print($"{gameObject.name} ÀÌ Purchase() Á¶°Ç ºÒÃæÁ·");
        }
    }
    public void UnLock()
    {
        print($"{gameObject.name} ÀÌ UnLock() µÊ");
        state = TechStateEnum.PurchaseEnabled;
        image = GetComponent<Image>();
        image.color = Color.gray;
    }
    public void Check()
    {
        bool checkUnlock = true;
        for (int i = 0; i < upTierTechs.Length; i++)
        {
            checkUnlock = upTierTechs[i].State == TechStateEnum.UnLcoked ? true : false;
        }
        if (GameManager.Instance.currentUser.isUnlockUpgrade[upgradeInfo.index])
        {
            Purchase();
            return;
        }
        if (checkUnlock)
        {
            UnLock();
        }
    }
}
public enum TechStateEnum
{
    UnLcoked,
    PurchaseEnabled,
    Locked,
    First
}