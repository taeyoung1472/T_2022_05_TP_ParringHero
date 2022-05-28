using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] Text text;
    public void On()
    {
        obj.SetActive(true);
        Time.timeScale = 0;
    }
    public void Off()
    {
        StartCoroutine(OffCor());
        //Time.timeScale = 1;
        //obj.SetActive(false);
    }
    public IEnumerator OffCor()
    {
        obj.SetActive(false);
        text.gameObject.SetActive(true);
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            for (int i = 3; i >= 1; i--)
            {
                text.text = i.ToString();
                yield return new WaitForSecondsRealtime(1f);
            }
        }
        yield return new WaitForSecondsRealtime(0.001f);
        Time.timeScale = 1;
        text.gameObject.SetActive(false);
    }
    public void Exit()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Time.timeScale = 1;
            obj.SetActive(false);
            Application.Quit();
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Time.timeScale = 1;
            obj.SetActive(false);
            SceneManager.LoadScene(0);
        }
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}