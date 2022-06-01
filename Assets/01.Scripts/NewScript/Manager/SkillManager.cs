using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private SkillType skillType;
    [SerializeField] private Button skillButton;
    [SerializeField] private Image skillButtonImage;
    public void Start()
    {
        if(skillType == SkillType.None)
        {

        }
    }
    public void Skill()
    {
        switch (skillType)
        {
            case SkillType.None:
                break;
            case SkillType.Meteo:
                break;
            case SkillType.Run:
                break;
            default:
                break;
        }
    }
}
