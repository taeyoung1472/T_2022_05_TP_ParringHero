using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
public class OverPanel : MonoBehaviour
{
    [SerializeField] private Text timeTxt, coinTxt;
    [SerializeField] private Button[] buttons;
    private float time = 0;
    private float coin = 0;
    public void Set(int _time, int _coin)
    {
        GameManager.Instance.currentUser.coin += _coin;
        GameManager.Instance.SaveUser();
        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => time, x => time = x, _time, 1));
        seq.Join(DOTween.To(() => coin, x => coin = x, _coin, 1));
        seq.AppendCallback(() => Time.timeScale = 0);
        seq.AppendCallback(() =>
        {
            foreach (var but in buttons)
            {
                but.interactable = true;
            }
        });
    }
    public void Update()
    {
        timeTxt.text = $"{time:#}";
        coinTxt.text = $"{coin:#}";
    }
}