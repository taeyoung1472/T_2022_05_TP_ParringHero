using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Text upgradeName;
    [SerializeField] private Text upGradeGold;
    [SerializeField] private Text upgradeDesc;
    [SerializeField] private Image upGradeImage;
    [SerializeField] private GameObject btn;
    [SerializeField] private TechTree[] teches;
    UpgradeInfo upgradeInfo;
    public void SetUI(UpgradeInfo info)
    {
        if(teches[info.index].State != TechStateEnum.Locked)
        {
            upgradeInfo = info;
            upGradeImage.sprite = upgradeInfo.sprite;
            upgradeName.text = upgradeInfo.name;
            upGradeGold.text = upgradeInfo.price.ToString();
            upgradeDesc.text = upgradeInfo.desc;
            btn.SetActive(true);
        }
    }
    public void Buy()
    {
        GameManager gameManager = GameManager.Instance;
        if (gameManager.Purchase(upgradeInfo.price) && !gameManager.currentUser.isUnlockUpgrade[upgradeInfo.index])
        {
            teches[upgradeInfo.index].Purchase();
            gameManager.currentUser.fixedDamage += upgradeInfo.damage;
            gameManager.currentUser.fixedMaxHP += upgradeInfo.hp;
            gameManager.currentUser.timePerHealHp += upgradeInfo.timePerHealHp;
            gameManager.currentUser.timePerCoin += upgradeInfo.timePerGetCoin;
            gameManager.currentUser.isUnlockUpgrade[upgradeInfo.index] = true;
            if (upgradeInfo.isReberse)
            {
                gameManager.currentUser.reberseChance++;
            }
            gameManager.currentUser.coin -= upgradeInfo.price;
        }
        else
        {
            print("µ·¾øÀ½");
        }
    }
}
