using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangePageButton : MonoBehaviour,IPointerUpHandler
{
    public GameObject curPage, nextPage;
    public bool pageState=true;
    private void Awake()
    {
        curPage.SetActive(pageState);
        //nextPage.SetActive(!pageState);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //点击后关闭并开启指定对象
        //pageState = !pageState;
        curPage.SetActive(!curPage.activeSelf);
        nextPage.SetActive(!nextPage.activeSelf);
    }
}
