using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private ShopInfo[] shopInfo;
    [SerializeField] private Transform trans;
    [SerializeField] private GameObject shopObject;
    void Start()
    {
        foreach (var item in shopInfo)
        {
            GameObject obj = Instantiate(shopObject, trans);
            obj.SetActive(true);
            obj.GetComponent<ShopLocalScript>().Set(item);
        }
    }
}
