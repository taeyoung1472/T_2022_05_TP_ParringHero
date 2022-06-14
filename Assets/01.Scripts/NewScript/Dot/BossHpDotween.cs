using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BossHpDotween : MonoBehaviour
{
    void Start()
    {
        Move();
    }
    private void Move()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(3.8f, 1f));
        seq.Append(transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1.2f));
        seq.Append(transform.DOScale(new Vector3(1f, 1f, 1f), 1.2f));
    }

}
