using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticPen : MonoBehaviour,IPointerUpHandler
{
    public GameObject dynamicPen;

    private void Start()
    {
        dynamicPen.SetActive(false);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        dynamicPen.SetActive(true);
        gameObject.SetActive(false);
    }
}
