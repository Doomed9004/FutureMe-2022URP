using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxSinglePassword : MonoBehaviour,IPointerUpHandler
{
    public int curPW = 0;
    public TextMeshPro tmp;

    public event Action OnChange;
    
    bool _canInteract = false;
    public int pwCount = 10;
    private void Awake()
    {
        if (GetComponent<EventTrigger>() == null)
            transform.AddComponent<EventTrigger>();
    }

    public int GetPassword()
    {
        return curPW;
    }

    public void SetCanInteract(bool b)
    {
        _canInteract = b;
        tmp.gameObject.SetActive(b);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        curPW = (curPW + 1) % pwCount;//密码最大数
        tmp.text=curPW.ToString();
        
        OnChange?.Invoke();
    }
}
