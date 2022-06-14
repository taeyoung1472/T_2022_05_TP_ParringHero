using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class DifficultyDotween : MonoBehaviour
{
    private Vector2 originPos = Vector2.zero;
   
    private void OnEnable()
    {
        originPos = transform.position;
        TextMove();
    }
    void TextMove()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(transform.position.y + Random.Range(1f,3f), 0.5f));
        seq.Insert(0, gameObject.GetComponent<TextMeshPro>().DOFade(1, 0.25f));
        seq.Append(transform.DOMoveY(transform.position.y - Random.Range(1f, 3f), 0.5f));
        seq.Append(transform.DOMoveY(originPos.y, 0.5f));
        seq.Append(gameObject.GetComponent<TextMeshPro>().DOFade(0, 0.25f));
        seq.AppendCallback(() => gameObject.SetActive(false));
    }
}
