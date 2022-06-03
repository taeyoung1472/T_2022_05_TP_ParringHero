using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
    
public class DifficultyManager : MonoSingleton<DifficultyManager>
{
    private float time = 0f;
    //public UnityEvent difficultyUp;

    public List<DifficultySO> difficultySO;
    private int diffcultyIndex = 0;
    //public int DiffcultyIndex { get => diffcultyIndex; }

    void Start()
    {
        GameManager.Instance.currentDifficultySO = difficultySO[diffcultyIndex];
    }

    void Update()
    {
        time += Time.deltaTime;    
        if (time >= 2)
        {
            diffcultyIndex++; 
            try
            {
                print("난이도 조종!");
                GameManager.Instance.currentDifficultySO = difficultySO[diffcultyIndex];
            }
            catch
            {
                //이미 최대 난이도
            }
            time = 0;
            /*switch (diffcultyIndex)
            {
                case 0:
                    difficultyUp?.Invoke();
                    time = 0f;
                    break;
                case 1:
                    difficultyUp?.Invoke();
                    time = 0f;
                    break;
                case 2:
                    difficultyUp?.Invoke();
                    time = 0f;
                    break;
            }*/
        }
    }
}
