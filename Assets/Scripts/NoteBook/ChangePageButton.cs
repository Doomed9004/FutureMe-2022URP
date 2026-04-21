using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangePageButton : MonoBehaviour,IPointerUpHandler
{
    public GameObject notePage, emptyPage;
    public bool pageState=true;
    private void Awake()
    {
        notePage.SetActive(true);
        emptyPage.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //点击后关闭并开启指定对象
        pageState = !pageState;
        notePage.SetActive(pageState);
        emptyPage.SetActive(!pageState);
    }
}
