using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class StageSystem : MonoBehaviour
{
    [SerializeField] private float[] stagePerTime;
    [SerializeField] private GameObject[] backGround;
    [SerializeField] private Slider slider;
    float time = 0;
    int stage = 0;
    public void Start()
    {
        ChangeStage();
        StartCoroutine(Stage());
    }
    public void Update()
    {
        slider.value = Time.time - time;
    }
    public void ChangeStage()
    {
        for (int i = 0; i < backGround.Length; i++)
        {
            backGround[i].SetActive(false);
        }
        backGround[stage].SetActive(true);
        slider.maxValue = stagePerTime[stage];
        stage++;
        time = Time.time;
    }
    public IEnumerator Stage()
    {
        while (stage < stagePerTime.Length)
        {
            yield return new WaitForSeconds(stagePerTime[stage]);
            ChangeStage();
        }
    }
}
