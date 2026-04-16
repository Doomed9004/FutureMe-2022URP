using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 offset;
    Camera mainCamera;
    [HideInInspector]
    public bool canDrag=true;
    private void Awake()
    {
        if (GetComponent<EventTrigger>() == null)
            transform.AddComponent<EventTrigger>();
        // if(GetComponent<Collider>() == null)
        //     transform.AddComponent<BoxCollider2D>();
        if (mainCamera == null)
            mainCamera = Camera.main;
    }


    public void OnDrag(PointerEventData eventData)
    {
        if(!canDrag)return;
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = mousePos-offset;
        transform.position=new Vector3(transform.position.x,transform.position.y,0);
        //Debug.Log(eventData.position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!canDrag)return;
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(eventData.position);
        offset = mousePos - transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    
}
