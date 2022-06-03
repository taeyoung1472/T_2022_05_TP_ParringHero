using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private Image skillProfile;
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Use());
    }
    void Charge()
    {
        button.interactable = true;
    }
    void Use()
    {
        skillProfile.fillAmount = 0;
        button.interactable = false;
        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => skillProfile.fillAmount, x => skillProfile.fillAmount = x, 1, 30));
        seq.AppendCallback(() => Charge());
    }
}
