using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogWindow : MonoBehaviour, IPointerUpHandler
{
    public event Action OnClick;

    private void Awake()
    {
        if (GetComponent<EventTrigger>() == null)
            transform.AddComponent<EventTrigger>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}