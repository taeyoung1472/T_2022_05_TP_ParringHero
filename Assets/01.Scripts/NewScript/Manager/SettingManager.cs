using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SettingManager : MonoBehaviour
{
    [SerializeField] private GameObject settingObject;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider MasterSoundSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider FXSlider;
    private void OnEnable()
    {
        MasterSoundSlider.value = PlayerPrefs.GetFloat("Master");
        bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        FXSlider.value = PlayerPrefs.GetFloat("FX");
    }
    public void ShowSetting(bool isShow)
    {
        settingObject.SetActive(isShow);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChangeBGMVolume(float volume)
    {
        if (volume <= -30f)
        {
            audioMixer.SetFloat("BGM", -80f);
        }
        else
        {
            audioMixer.SetFloat("BGM", volume);
            PlayerPrefs.SetFloat("BGM", volume);
        }
    }
    public void ChangeMasterVolume(float volume)
    {
        Debug.Log(volume);
        if (volume <= -30)
        {
            audioMixer.SetFloat("Master", -80f);
        }
        else
        {
            audioMixer.SetFloat("Master", volume);
        }
        PlayerPrefs.SetFloat("Master", volume);
    }
    public void ChangeSFXVolume(float volume)
    {
        if (volume <= -30)
        {
            audioMixer.SetFloat("FX", -80f);
        }
        else
        {
            audioMixer.SetFloat("FX", volume);
        }
        PlayerPrefs.SetFloat("FX", volume);
    }
    public void Exit()
    {
        gameObject.SetActive(false);
    }
}