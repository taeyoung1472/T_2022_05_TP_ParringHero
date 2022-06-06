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

    [SerializeField]
    private GameObject diffifultyTextPrefab;

    [SerializeField]
    private float[] atkDelay = new float[5];

    [SerializeField]
    private float[] parringAtktime= new float[5];

    void Start()
    {
        GameManager.Instance.currentDifficultySO = difficultySO[diffcultyIndex];
    }

    void Update()
    {
        time += Time.deltaTime;    
        if (time >= 20)
        {
            diffcultyIndex++; 
            try
            {
                print("난이도 조종!");
                GameManager.Instance.currentDifficultySO = difficultySO[diffcultyIndex];
                EnemyBase.attackDelayTime = atkDelay[diffcultyIndex]; 
                EnemyBase.parringAtkTime = parringAtktime[diffcultyIndex];
                StartCoroutine(CreatText());
            }
            catch
            {
                //이미 최대 난이도
            }
            time = 0;
        }
    }
    IEnumerator CreatText()
    {
        print("텍스트잘나옴");
        diffifultyTextPrefab.gameObject.SetActive(true);
        CamManager.Instance.SetCamShake(0.5f, 3f);
        yield return new WaitForSeconds(1.5f);
        diffifultyTextPrefab.gameObject.SetActive(false);
    }

}
