using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class BossAprroachText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bossNameText;
    [SerializeField] private Vector3 aprroachPos;
    [SerializeField] private Vector3 disAprroachPos;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Aprroach();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            DisAprroach();
        }
    }
    public void Aprroach()
    {
        transform.DOLocalMove(aprroachPos, 0.5f);
    }
    public void DisAprroach()
    {
        transform.DOLocalMove(disAprroachPos, 0.5f);
    }
}
