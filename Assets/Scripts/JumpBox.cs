using UnityEngine;
using UnityEngine.EventSystems;

public class JumpBox : JumpBase,IInteractable,IPointerDownHandler,IPointerUpHandler
{
    public void OnInteract()
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(name+"OnPointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Jump(scene);
        Debug.Log(name+"OnPointerUp");
    }
}
