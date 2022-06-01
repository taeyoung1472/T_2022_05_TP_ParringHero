using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DifficultyManager : MonoBehaviour
{
    private float time = 0f;
    public UnityEvent difficultyUp;
    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        Debug.Log(time);
        if (time >= 2)
        {
            difficultyUp?.Invoke();
            time = 0f;
        }
    }
}
