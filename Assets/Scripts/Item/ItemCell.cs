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
    

    private void Awake()
    {
        button.onClick.AddListener(()=>
        {
            ItemManager.Ins.ChangeCurrentItem(itemData, this,iconImage.transform);
        });
    }

    public void UpdateCell(ItemData itemData)
    {
        if (itemData == null)
        {
            Debug.Log("icon为空");
            iconImage.sprite = null;
            this.itemData = itemData;
            
            return;
        }
        iconImage.sprite = itemData.icon;
        this.itemData = itemData;
    }

    public Transform GetParent()
    {
        return iconImage.transform;
    }

    public ItemData GetItem()
    {
        return itemData;
    }
}
