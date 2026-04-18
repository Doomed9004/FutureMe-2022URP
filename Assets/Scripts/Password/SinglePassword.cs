using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SinglePassword : MonoBehaviour,IPointerUpHandler
{
    public GuitarPassword curPW = GuitarPassword.E;
    public TextMeshPro tmp;
    public string aniName;
    public Animator animator;

    public event Action OnChange;
    
    bool _canInteract = false;
    private void Awake()
    {
        if (GetComponent<EventTrigger>() == null)
            transform.AddComponent<EventTrigger>();
        
        tmp.gameObject.SetActive(false);
    }

    public GuitarPassword GetPassword()
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
        if(!_canInteract)return;
        
        curPW = (GuitarPassword)(((int)curPW + 1) % 5);//这里按照枚举数量硬编码5
        tmp.text=curPW.ToString();
        
        animator.Play(aniName);
        
        OnChange?.Invoke();
    }
}

public enum GuitarPassword
{
    E,A,D,G,B
}
