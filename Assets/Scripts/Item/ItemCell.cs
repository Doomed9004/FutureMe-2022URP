using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour
{
    [SerializeField]Image iconImage;
    [FormerlySerializedAs("numText")] [SerializeField]TextMeshProUGUI nameText;
    [FormerlySerializedAs("item")] [SerializeField]ItemData itemData;
    [SerializeField]Button button;
    
    Color transparent=new Color(0, 0, 0, 0);
    private void Awake()
    {
        button.onClick.AddListener(()=>
        {
            ItemManager.Ins.ChangeCurrentItem(itemData, this,transform);
        });
        iconImage.color = new Color(0, 0, 0, 0);
    }

    public void UpdateCell(ItemData itemData)
    {
        if (itemData == null)
        {
            Debug.Log("icon为空");
            iconImage.sprite = null;
            this.itemData = itemData;
            iconImage.color=transparent;
            
            return;
        }
        iconImage.sprite = itemData.icon;
        this.itemData = itemData;
        iconImage.color=Color.white;
    }

    public Transform GetParent()
    {
        return transform;
    }

    public ItemData GetItem()
    {
        return itemData;
    }
}
