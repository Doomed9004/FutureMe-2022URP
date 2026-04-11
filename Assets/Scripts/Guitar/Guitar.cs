using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Guitar : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private GuitarState curState = GuitarState.Misshapen;
    public ItemData misshapenItem;
    public MultiPassword multiPassword;
    bool isTuned = false;
    enum GuitarState
    {
        Misshapen,
        NotTuned,
        Complete
    }

    private void Awake()
    {
        multiPassword.Unlock += Tuned;
    }

    void Tuned()
    {
        //TODO:隐藏调弦界面
        
        curState = GuitarState.Complete;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (curState)
        {
            case GuitarState.Misshapen:
                if (ItemManager.Ins.curItemData == misshapenItem)
                {
                    //TODO:上弦，显示调弦界面
                    
                    curState = GuitarState.NotTuned;
                }
                break;
            case GuitarState.Complete:
                //TODO:展示提示信息
                break;
        }
    }
}
