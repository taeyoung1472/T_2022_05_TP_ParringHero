using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class FocusPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject focusObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        focusObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        focusObject.SetActive(false);
    }
}
