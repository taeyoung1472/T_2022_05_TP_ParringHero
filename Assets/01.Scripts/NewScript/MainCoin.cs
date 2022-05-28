using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class MainCoin : MonoBehaviour
{
    [SerializeField] private Text coinTxt;
    void Start()
    {
        UpdateCoinUI();
    }
    public void UpdateCoinUI()
    {
        coinTxt.text = string.Format("{0:N0}", GameManager.Instance.currentUser.coin);
    }
}