using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class TechTree : MonoBehaviour
{
    public TechStateEnum State { get { return state; } set { state = value; } }
    [SerializeField] private UpgradeInfo upgradeInfo;
    [SerializeField] protected List<TechTree> upTierTechs = new List<TechTree>();
    [SerializeField] protected List<TechTree> downTierTechs = new List<TechTree>();
    [SerializeField] protected List<TechTree> cantChoiceWithTechs = new List<TechTree>();
    [SerializeField] protected TechStateEnum state = TechStateEnum.Locked;
    public UpgradeInfo UpgradeInfo { get { return upgradeInfo; } }
    Image image;
    public void Reset()
    {
        upTierTechs.Clear();
        downTierTechs.Clear();
        if(transform.parent.GetComponent<TechTree>() != null)
        {
            upTierTechs.Add(transform.parent.GetComponent<TechTree>());
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            try
            {
                downTierTechs.Add(transform.GetChild(i).GetComponent<TechTree>());
            }
            catch
            {
                //그냥 테크트리 없을때
            }
        }
    }
    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => UpgradeManager.instance.SetUI(this));
        image = GetComponent<Image>();
        if (!GameManager.Instance.upgradeBoolDic.ContainsKey(upgradeInfo.name))
        {
            GameManager.Instance.upgradeBoolDic.Add(upgradeInfo.name, false);
        }
        if (GameManager.Instance.upgradeBoolDic[upgradeInfo.name] && state == TechStateEnum.First)
        {
            print("처음거가 됨!!!");
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
        if(state == TechStateEnum.PurchaseEnabled || state == TechStateEnum.First || GameManager.Instance.upgradeBoolDic[upgradeInfo.name])
        {
            print($"{upgradeInfo.name} 이가 구매완료됨!");
            state = TechStateEnum.UnLcoked;
            image = GetComponent<Image>();
            image.color = Color.white;
            GameManager.Instance.upgradeBoolDic[upgradeInfo.name] = true;
            foreach (TechTree tech in downTierTechs)
            {
                tech.Check();
            }
            print($"{gameObject.name} 이 Purchase() 됨");
        }
        else
        {
            print($"{gameObject.name} 이 Purchase() 조건 불충족");
        }
    }
    public void UnLock()
    {
        print($"{gameObject.name} 이 UnLock() 됨");
        state = TechStateEnum.PurchaseEnabled;
        image = GetComponent<Image>();
        image.color = Color.gray;
    }
    public void Check()
    {
        bool checkUnlock = true;
        foreach (TechTree tech in upTierTechs)
        {
            checkUnlock = tech.state == TechStateEnum.UnLcoked ? true : false;
        }
        if (GameManager.Instance.upgradeBoolDic[upgradeInfo.name])
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