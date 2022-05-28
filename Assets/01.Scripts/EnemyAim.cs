using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoSingleton<EnemyAim>
{

    Animator anim = null;

    private readonly int hashDetact = Animator.StringToHash("Detect");
    private readonly int hashEyeSpeed = Animator.StringToHash("EyeSpeed");



    public void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetEyeAnime() 
    {
        anim.SetTrigger(hashDetact);
    }

    public void SetEyeSpeed(float value)
    {
        anim.SetFloat(hashEyeSpeed, value);
    }
}
