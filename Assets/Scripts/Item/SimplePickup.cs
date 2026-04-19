using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimplePickup : MonoBehaviour,IPointerUpHandler
{
    //基础拾取物品 
    [SerializeField]ItemData itemData;
    
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("拾取物品："+itemData.name);
        if(itemData.clickSound!=null)
            AudioSource.PlayClipAtPoint(itemData.clickSound, transform.position);
        
        ItemManager.Ins.AddItem(itemData);
        
        Destroy(gameObject);
    }
}
