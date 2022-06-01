using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    [Header("UI 관련")]
    [SerializeField] private Text upgradeName;
    [SerializeField] private Text upGradeGold;
    [SerializeField] private Text upgradeDesc;
    [SerializeField] private Image upGradeImage;
    [SerializeField] private GameObject btn;
    [Header("테크트리")]
    [SerializeField] private TechTree[] tech;
    Dictionary<string, TechTree> teches = new Dictionary<string, TechTree>();//TechTree[] teches;
    UpgradeInfo upgradeInfo;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        Init();
    }
    void Init()
    {
        foreach (TechTree data in tech)
        {
            teches.Add(data.UpgradeInfo.name, data);
        }
    }
    public void SetUI(TechTree tech)
    {
        if(teches[tech.UpgradeInfo.name].State != TechStateEnum.Locked)
        {
            upgradeInfo = tech.UpgradeInfo;
            upGradeImage.sprite = tech.GetComponent<Image>().sprite;//.sprite;
            upgradeName.text = upgradeInfo.name;
            upGradeGold.text = upgradeInfo.price.ToString();
            upgradeDesc.text = upgradeInfo.desc;
            btn.SetActive(true);
        }
    }
    public void Test()
    {
        print("Test!");
    }
    public void Buy()
    {
        GameManager gameManager = GameManager.Instance;
        if (gameManager.Purchase(upgradeInfo.price))// && !gameManager.currentUser.isUnlockUpgrade[upgradeInfo.index])
        {
            print("업그레이드");
            teches[upgradeInfo.name].Purchase();
            gameManager.currentUser.timePerHealHp += upgradeInfo.upgrade.timePerHp;
            gameManager.currentUser.timePerCoin += upgradeInfo.upgrade.timePerCoin;
            if (upgradeInfo.upgrade.isRevive)
            {
                gameManager.currentUser.reberseChance++;
            }
            gameManager.currentUser.coin -= upgradeInfo.price;
        }
        else
        {
            print("돈없음");
        }
    }
}
