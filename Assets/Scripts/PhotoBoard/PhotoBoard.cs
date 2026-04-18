using System;
using UnityEngine;

public class PhotoBoard : MonoBehaviour
{
    public InteractablePhoto[] photos;

    public event Action Unlock;

    private void Awake()
    {
        foreach (InteractablePhoto photo in photos)
        {
            photo.Complete += Check;
        }
    }

    void Check()
    {
        foreach (InteractablePhoto photo in photos)
        {
            if (!photo.GetState()) return;//照片整理未完成
        }
        
        //照片整理完成
        //TODO：解锁新对话，解锁写信功能
        Unlock?.Invoke();
    }
}
