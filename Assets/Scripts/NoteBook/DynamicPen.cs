using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicPen : MonoBehaviour,IDragHandler
{
    public Transform drawPoint;
    public Camera drawCamera;
    public Brush brush;
    public float depth=-.1f;

    // private void Start()
    // {
    //     drawCamera.gameObject.SetActive(false);
    // }

    private void OnEnable()
    {
        drawCamera.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (drawCamera == null)return;
        drawCamera.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        brush.Draw(drawPoint.position,depth);
    }
}
