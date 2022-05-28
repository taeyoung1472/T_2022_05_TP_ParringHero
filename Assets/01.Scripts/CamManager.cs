using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class CamManager : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    CinemachineBasicMultiChannelPerlin cmPerlin;

    Tween camTween = null;

    void Start()
    {
        cmPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    public void SetCamShake(float duration, float power = 5f)
    {
        if (camTween != null && camTween.IsActive())
        {
            camTween.Kill();
        }

        cmPerlin.m_AmplitudeGain = power;
        camTween = DOTween.To(
            () => cmPerlin.m_AmplitudeGain,
            value => cmPerlin.m_AmplitudeGain = value, 0, duration);
    }
}
