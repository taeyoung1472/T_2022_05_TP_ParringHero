using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HitFeel : MonoSingleton<HitFeel>
{
    public Light2D light2d;
    private void Awake()
    {
        light2d = FindObjectOfType<Light2D>();
    }
    private void Update()
    {
      
    }
    public void LightCon()
    {
        light2d.color = new Color(1f, 0.2f, 0.2f);
        light2d.intensity = 3f;
        Debug.Log("라이트");
    }    
     public IEnumerator ChangeLight()
    {
        LightCon();
        yield return new WaitForSeconds(2f);
        DefalutCon();
    }
    public void ASDF()
    {
        StartCoroutine(ChangeLight());
    }
    public void DefalutCon()
    {
        light2d.color = new Color(1f, 1f, 1f);
        light2d.intensity = 1f;
        Debug.Log("라이트");
    }
}
