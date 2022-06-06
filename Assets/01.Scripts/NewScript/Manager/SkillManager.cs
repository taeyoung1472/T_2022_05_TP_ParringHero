using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private SkillUI[] skillUi;
    [SerializeField] private GameObject meteo;
    public void Start()
    {
        foreach (SkillUI item in skillUi)
        {
            foreach (SkillType type in GameManager.Instance.currentUser.skillTypes)
            {
                if(item.SkillType == type)
                {
                    item.EnableSkill();
                }
            }
        }
    }
    public void Skill(SkillType _type)
    {
        switch (_type)
        {
            case SkillType.None:
                break;
            case SkillType.Meteo:
                for (int i = 0; i < 25; i++)
                {
                    Instantiate(meteo);
                }
                break;
            case SkillType.Run:
                break;
            default:
                break;
        }
    }
}
