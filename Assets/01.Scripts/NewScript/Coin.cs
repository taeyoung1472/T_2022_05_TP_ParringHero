using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ISound
{
    [SerializeField] private CoinUI coinUI;
    [SerializeField] private int getCoin;

    public void PlaySound(AudioClip clip, float volume = 1)
    {
        throw new System.NotImplementedException();
    }

    //[SerializeField] private float getDelay;
    void Start()
    {
        coinUI = FindObjectOfType<CoinUI>();
        coinUI.GetCoin(getCoin);
        //StartCoroutine(GetCoin());
    }
    /*IEnumerator GetCoin()
    {
        yield return new WaitForSeconds(getDelay);
        coinUI.GetCoin(getCoin);
    }*/
}
