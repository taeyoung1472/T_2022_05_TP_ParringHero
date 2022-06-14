using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class CamManager : MonoSingleton<CamManager>
{
    public CinemachineVirtualCamera vCam;
    CinemachineBasicMultiChannelPerlin cmPerlin;
    CinemachineComponentBase componentBase;
    public float _minmum = 3.5f;
    public float _maximun = 8f;
    static float t = 0f;
    Tween camTween = null;

    void Start()
    {
        cmPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        componentBase = vCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
        _minmum = vCam.m_Lens.OrthographicSize;
    }
    public void Update()
    {
      
    }
    public void ZoomInOut()
    {
        StopAllCoroutines();
        StartCoroutine(Zoom());
    }
    IEnumerator Zoom()
    {
        StartCoroutine(Lerp(vCam.m_Lens.OrthographicSize, _maximun));
        yield return new WaitForSeconds(1f);
        StartCoroutine(Lerp(vCam.m_Lens.OrthographicSize, _minmum));
    }
    IEnumerator Lerp(float start, float end)
    {
        t = 0f;
        while (vCam.m_Lens.OrthographicSize != end)
        {
            vCam.m_Lens.OrthographicSize = Mathf.Lerp(start, end, t);
            t += Time.deltaTime;
            yield return null;
        }
        yield return null;
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
