using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class CoinUI : MonoBehaviour
{
    public int CoinValue { set { coinValue = value; } get { return coinValue; } }
    public int Coin { get { return coin; } }
    [SerializeField] private Text coinText;
    private int coin = 0;
    int coinValue = 1;
    public void Start()
    {
        if (GameManager.Instance.IsMenu)
        {
            coin = GameManager.Instance.currentUser.coin;
            AddCoin(0);
            return;
        }
        StartCoroutine(GetCoinPerSecond());
    }
    public void AddCoin(int value)
    {
        coin += value;
        coinText.text = string.Format("{0:N0}",coin);
    }
    IEnumerator GetCoinPerSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            AddCoin(coinValue);
        }
    }
    IEnumerator GetCoinCor(int get)
    {
        for (int i = 0; i < get; i++)
        {
            yield return new WaitForSeconds(1 / get);
            AddCoin(1);
        }
    }
    public void GetCoin(int get)
    {
        StartCoroutine(GetCoinCor(get));
    }
}