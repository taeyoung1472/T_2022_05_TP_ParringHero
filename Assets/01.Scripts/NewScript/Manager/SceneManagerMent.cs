using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class SceneManagerMent : MonoBehaviour
{
    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        GameManager.Instance.SaveUser();
        SceneManager.LoadScene(index);
        /*if (!GameManager.Instance.currentUser.isTutoClear)
        {
            SceneManager.LoadScene(2);
        }*/
    }
    public void ExitGame()
    {
        GameManager.Instance.SaveUser();
        Application.Quit();
    }
}