using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
public class DialogManager : MonoBehaviour
{
    public static DialogManager instance = null;

    public static Transform Player
    {
        get
        {

            return instance.player;

        }
    }


    public Transform player;
    //public GameObject bloodParticlePrefab;
    public DialogPanel dialogPanel; //���̾� �α� �г� ����� ��ũ��Ʈ
    private Dictionary<int, List<TextVo>> dialogTextDictionary = new Dictionary<int, List<TextVo>>();

    //�ϴ�

    private float timeScale = 1f;

    public static float TimeScale
    {
        get
        {
            return instance.timeScale;
        }
        set
        {
            instance.timeScale = Mathf.Clamp(value, 0, 1);
        }
    }
    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogError("<color Hccc> WARN </color> : GameManagerInstance is not thesu");
        }
        instance = this;
        TextAsset dJson = Resources.Load("msg") as TextAsset;
        GameTextDate textDate = JsonUtility.FromJson<GameTextDate>(dJson.ToString());
        //GameTextDate �������� �װ� ���� ����Ʈ�� ���� �ǰ�?

        foreach (DialogVO vo in textDate.list)
        { 
            dialogTextDictionary.Add(vo.code, vo.text); 
     
        }



    }



    public static void ShowDialog(int index, Action callback = null)
    {
        //GameManager.Instance.currentUser.isTutoClear = true;
        GameManager.Instance.SaveUser();
        if (index >= instance.dialogTextDictionary.Count)
        {
            return; //����Ʈ�� �ִ� �ͺ��� ���ٸ� �߸���û�� ���̹Ƿ�
        }
        instance.dialogPanel.StartDialog(instance.dialogTextDictionary[index], callback);
    }
    public static void End()
    {
        SceneManager.LoadScene(0);
    }
}
