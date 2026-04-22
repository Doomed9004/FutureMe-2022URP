using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhotoBoard : MonoBehaviour,IPointerUpHandler
{
    public InteractablePhoto[] photos;
    [SerializeField] private ItemData[] needItem;
    
    [Header("对话控制与功能解锁")]
    public DialogBox dialogBox;
    public DialogList newDialog;
    public NoteBook noteBook;
    
    
    public event Action Unlock;

    private Dictionary<ItemData, GameObject> photo=new Dictionary<ItemData, GameObject>();
    private void Awake()
    {
        foreach (InteractablePhoto photo in photos)
        {
            photo.Complete += Check;
        }

        for (int i = 0; i < photos.Length; i++)
        {
            photo[needItem[i]]=photos[i].gameObject;
            photos[i].gameObject.SetActive(false);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        ItemData item = ItemManager.Ins.curItemData;
        if (item == null) return;
        if (photo.TryGetValue(item, out GameObject obj))
        {
            obj.SetActive(true);
            ItemManager.Ins.DestroyItem();
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
        dialogBox.ShowDialog(newDialog);//显示新对话
        noteBook.SetCanWrite(true);//解锁写信功能
        
        Unlock?.Invoke();
    }

    [ContextMenu("解锁测试")]
    public void UnlockTest()
    {
        dialogBox.ShowDialog(newDialog);//显示新对话
        noteBook.SetCanWrite(true);//解锁写信功能
        
        Unlock?.Invoke();
    }
}
