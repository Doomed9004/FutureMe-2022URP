using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractablePhoto : MonoBehaviour,IEndDragHandler
{
    [SerializeField]GameObject target;
    bool getTarget = false;
    DragObject dragObject;
    public event Action Complete;
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!getTarget)return;
        dragObject.canDrag = false;
        transform.position = target.transform.position;
        Complete?.Invoke();
    }

    private void Start()
    {
        dragObject=GetComponent<DragObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject!=target)return;
        getTarget = true;
        Debug.Log("OnTriggerEnter2D");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject!=target)return;
        getTarget = false;
    }

    public bool GetState()
    {
        return getTarget;
    }
}
