using System;
using UnityEngine;

public class SimplePickup : MonoBehaviour,IInteractable
{
    //基础拾取物品 
    [SerializeField]ItemData itemData;

    public void OnInteract(ItemData heldItem)
    {
        Debug.Log("拾取物品："+itemData.name);
        if(itemData.clickSound!=null)
            AudioSource.PlayClipAtPoint(itemData.clickSound, transform.position);
        
        ItemManager.Ins.AddItem(itemData);
        
        Destroy(gameObject);
    }
}
