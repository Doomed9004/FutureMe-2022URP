using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogWindow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnClick;

    private void Awake()
    {
        if (GetComponent<EventTrigger>() == null)
            transform.AddComponent<EventTrigger>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}