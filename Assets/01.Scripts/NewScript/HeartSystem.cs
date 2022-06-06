using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class HeartSystem : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    [SerializeField] private Transform trans;
    private GameObject[] objects;
    public void Set(int _maxHp)
    {
        objects = new GameObject[_maxHp];
        for (int i = 0; i < _maxHp; i++)
        {
            GameObject obj = Instantiate(heart, trans);
            obj.SetActive(true);
            objects[i] = obj;
        }
    }
    public void Heart(int _hp)
    {
        for (int i = objects.Length - 1; i >= _hp; i--)
        {
            try
            {
                objects[i].GetComponent<Image>().color = Color.gray;
            }
            catch
            {
                //¹üÀ§ ¹þ¾î³²
            }
        }
        for (int i = 0; i < _hp; i++)
        {
            objects[i].GetComponent<Image>().color = Color.white;
        }
    }
}