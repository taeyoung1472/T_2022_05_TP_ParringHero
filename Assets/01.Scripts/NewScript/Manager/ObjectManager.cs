using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class ObjectManager : MonoBehaviour
{
    public void ResetObject(Transform trans)
    {
        print("A");
        for (int i = 0; i < trans.childCount; i++)
        {
            trans.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void Activate_True(GameObject obj)
    {
        print("B");
        obj.SetActive(true);
    }
    public void Activate_False(GameObject obj)
    {
        print("C");
        obj.SetActive(false);
    }
    public void ResetAlpha(Transform trans)
    {
        print("A");
        for (int i = 0; i < trans.childCount; i++)
        {
            Image image = trans.GetChild(i).GetComponent<Image>();
            image.color = new Vector4(image.color.r, image.color.g, image.color.b, 0.5f);
        }
    }
    public void Alpha_True(Image image)
    {
        image.color = new Vector4(image.color.r, image.color.g, image.color.b, 1);
    }
}