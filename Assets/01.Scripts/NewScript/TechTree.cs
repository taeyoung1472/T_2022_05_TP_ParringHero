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
                //�׳� ��ũƮ�� ������
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
            print("ó���Ű� ��!!!");
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
            print($"{upgradeInfo.name} �̰� ���ſϷ��!");
            state = TechStateEnum.UnLcoked;
            image = GetComponent<Image>();
            image.color = Color.white;
            GameManager.Instance.upgradeBoolDic[upgradeInfo.name] = true;
            foreach (TechTree tech in downTierTechs)
            {
                tech.Check();
            }
            print($"{gameObject.name} �� Purchase() ��");
        }
        else
        {
            print($"{gameObject.name} �� Purchase() ���� ������");
        }
    }
    public void UnLock()
    {
        print($"{gameObject.name} �� UnLock() ��");
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