using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class UpGradeLocal : MonoBehaviour
{
    bool isUnLock = false;
    Button btn;
    [SerializeField] private int index;
    [SerializeField] private GameObject lockObject, shinyObject;
    private void Start()
    {
        btn = GetComponent<Button>();
        isUnLock = GameManager.Instance.currentUser.isUnlockUpgrade[index];
        CheckUnLock();
    }
    public void CheckUnLock()
    {
        lockObject.SetActive(!isUnLock);
        shinyObject.SetActive(isUnLock);
        btn.interactable = !isUnLock;
    }
    public void UnLock()
    {
        isUnLock = true;
        GameManager.Instance.currentUser.isUnlockUpgrade[index] = isUnLock;
        GameManager.Instance.SaveUser();
        CheckUnLock();
    }
}