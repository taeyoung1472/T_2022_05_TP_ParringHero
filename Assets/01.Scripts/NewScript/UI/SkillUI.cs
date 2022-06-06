using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private Image skillProfile;
    [SerializeField] private SkillType skillType;
    SkillManager skillManager;
    bool isEnableSkill = false;
    public SkillType SkillType { get { return skillType; } }
    Button button;
    private void Awake()
    {
        skillManager = FindObjectOfType<SkillManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Use());
    }
    [ContextMenu("이름설정")]
    public void SetName()
    {
        name = $"SkillButton({skillType})";
    }
    void Charge()
    {
        if (!isEnableSkill) return; 
        button.interactable = true;
    }
    void Use()
    {
        if (!isEnableSkill) return;
        skillManager.Skill(SkillType);
        skillProfile.fillAmount = 0;
        button.interactable = false;
        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => skillProfile.fillAmount, x => skillProfile.fillAmount = x, 1, 30));
        seq.AppendCallback(() => Charge());
    }
    public void EnableSkill()
    {
        isEnableSkill = true;
        skillProfile.fillAmount = 1;
        button.interactable = true;
    }
}
