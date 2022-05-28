using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class ShopLocalScript : MonoBehaviour
{
    [SerializeField] private Button buyBtn, useBtn;
    [SerializeField] private Image profileImage;
    [SerializeField] private Text nameText, descText, priceText;
    [SerializeField] private ShopInfo shopInfo;
    [SerializeField] private SetAnimationManager animationManager;
    public void Start()
    {
        Set(shopInfo);
    }
    public void Set(ShopInfo shop)
    {
        shopInfo = shop;
        profileImage.sprite = shopInfo.sprite;
        nameText.text = shopInfo.name;
        descText.text = shopInfo.desc;
        priceText.text = shopInfo.price.ToString();
        switch (shopInfo.productType)
        {
            case ProductType.Character:
                if (GameManager.Instance.currentUser.isUnlockCharacter[shopInfo.index])
                {
                    buyBtn.gameObject.SetActive(false);
                    useBtn.gameObject.SetActive(true);
                }
                break;
            case ProductType.Pet:
                if (GameManager.Instance.currentUser.isUnlockPet[shopInfo.index])
                {
                    buyBtn.gameObject.SetActive(false);
                    useBtn.gameObject.SetActive(true);
                }
                break;
        }
    }
    public void Buy()
    {
        if (GameManager.Instance.Purchase(shopInfo.price))
        {
            switch (shopInfo.productType)
            {
                case ProductType.Character:
                    GameManager.Instance.currentUser.isUnlockCharacter[shopInfo.index] = true;
                    break;
                case ProductType.Pet:
                    GameManager.Instance.currentUser.isUnlockPet[shopInfo.index] = true;
                    break;
            }
            buyBtn.gameObject.SetActive(false);
            useBtn.gameObject.SetActive(true);
            print(shopInfo.price);
        }
        else
        {
            print("µ·¾øÀ½");
        }
    }
    public void Use()
    {
        switch (shopInfo.productType)
        {
            case ProductType.Character:
                print("ÄÉ¸¯ÅÍ");
                GameManager.Instance.currentUser.playerIndex = shopInfo.index;
                break;
            case ProductType.Pet:
                print("Æê");
                GameManager.Instance.currentUser.petIndex = shopInfo.index;
                GameManager.Instance.currentUser.petMaxHp = shopInfo.MaxHp;
                GameManager.Instance.currentUser.petDamage = shopInfo.Damage;
                GameManager.Instance.currentUser.petTimePetHeal = shopInfo.TimePetHeal;
                GameManager.Instance.currentUser.petTimePerCoin = shopInfo.TimePerCoin;
                break;
        }
        GameManager.Instance.SaveUser();
        animationManager.Set();
    }
}
