using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SinglePassword : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public GuitarPassword curPW = GuitarPassword.E;

    public event Action OnChange;
    private void Awake()
    {
        if (GetComponent<EventTrigger>() == null)
            transform.AddComponent<EventTrigger>();
    }

    public GuitarPassword GetPassword()
    {
        return curPW;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        curPW = (GuitarPassword)(((int)curPW + 1) % 5);//这里按照枚举数量硬编码5
        OnChange?.Invoke();
    }
}

public enum GuitarPassword
{
    E,A,D,G,B
}
