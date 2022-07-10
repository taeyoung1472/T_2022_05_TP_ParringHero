using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class CamManager : MonoSingleton<CamManager>
{
    public CinemachineVirtualCamera defaultCam;
    public CinemachineVirtualCamera bossFocusCam;
    public BossAprroachText bossAprroachText;
    public AudioClip clip;
    CinemachineBasicMultiChannelPerlin cmPerlin;
    Tween camTween = null;
    void Start()
    {
        cmPerlin = defaultCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ZoomInOut();        
        }
    }
    public void ZoomInOut()
    {
        PoolManager_Test.instance.Pop(PoolType.Sound).GetComponent<AudioPool>().PlayAudio(clip, 2, 0.75f);
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            defaultCam.gameObject.SetActive(false);
            bossFocusCam.gameObject.SetActive(true);
        });
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() => bossAprroachText.Aprroach());
        seq.AppendInterval(2f);
        seq.AppendCallback(() =>
        {
            defaultCam.gameObject.SetActive(true);
            bossFocusCam.gameObject.SetActive(false);
        });
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() => bossAprroachText.DisAprroach());
    }
    /*IEnumerator Lerp(float start, float end)
    {
        t = 0f;
 
        while (defaultCam.m_Lens.OrthographicSize != end)
        {
            defaultCam.m_Lens.OrthographicSize = Mathf.Lerp(start, end, t);
            t += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }*/

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
