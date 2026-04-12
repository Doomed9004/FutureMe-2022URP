using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemManager : MonoBehaviour
{
    private static ItemManager _instance;

    public static ItemManager Ins
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ItemManager>();
            }

            if (_instance == null)
            {
                GameObject obj = new GameObject("CameraManager");
                _instance = obj.AddComponent<ItemManager>();
            }

            return _instance;
        }
    }

    public ItemData curItemData;
    public ItemCell curItemCell;
    [SerializeField] List<ItemData> items;

    [Space(10)]
    //UI相关组件
    [SerializeField]
    GameObject itemUIPrefab;

    [SerializeField] Transform contentTrans;
    [SerializeField] Transform indicator;
    [SerializeField] List<ItemCell> itemCells = new List<ItemCell>();

    public AudioClip defaultClickSound;
    private int childCount;

    Dictionary<ItemCell, ItemData> ItemDict = new Dictionary<ItemCell, ItemData>();
    int itemCount = 0;

    private void Start()
    {
        //childCount = contentTrans.childCount;
        foreach (var i in itemCells)
        {
            ItemDict[i] = null;
        }
    }

    // public void UseItem(ItemData itemData)
    // {
    //     if (!itemData.canReuse)//物品不能重复使用 销毁并刷新
    //     {
    //         DestroyItem(itemData);
    //     }
    // }

    public void AddItem(ItemData itemData)
    {
        //items.Add(itemData);
        RefreshUI(itemData, true);
    }

    public void DestroyItem(ItemData itemData)
    {
        // if (curItemData.displayName == itemData.displayName)
        //     curItemData = null;
        // items.Remove(items.Find(t => t.displayName == itemData.displayName));
        if (curItemData == itemData)
            RefreshUI(itemData, false);
        else
            return;
    }

    void RefreshUI(ItemData itemData, bool isAdd)
    {
        if (isAdd && itemCount >= ItemDict.Count) return;//throw new Exception("物品数量超出范围");
        if (!isAdd && itemCount < 1) return;//throw new Exception("物品数量超出范围");

        ItemCell cell;
        if (!isAdd)
        {
            //删掉目标ItemData
            itemCount--;
            curItemCell.UpdateCell(null);
            ItemDict[curItemCell] = null;
            curItemCell.transform.SetAsLastSibling();
            //把ItemCell从列表中也挪到最后一个
            cell = curItemCell;
            Debug.Log(cell);
            itemCells.Remove(cell);
            itemCells.Add(cell);
            
            return;
        }

        //更改最后一个Cell的Data
        itemCount++;
        cell = itemCells[itemCount - 1];
        ItemDict[cell] = itemData;
        //更新Cell
        cell.UpdateCell(itemData);
        #region MyRegion

        // int count = items.Count - childCount;
        // if(count == 0)return;
        //
        // if (count > 0)
        // {
        //     //生成cell
        //     for (int i = 0; i < count; i++)
        //     {
        //         childCount++;
        //         ItemCell cell = Instantiate(itemUIPrefab, contentTrans).GetComponent<ItemCell>();
        //         itemCells.Add(cell);
        //     }
        //
        //     for (int i = 0; i < itemCells.Count; i++)
        //     {
        //         itemCells[i].SetValue(items[i],i);
        //     }
        // }
        //
        // if (count < 0)
        // {
        //     count = Mathf.Abs(count);
        //     //销毁cell
        //     for (int i = 0; i <count; i++)
        //     {
        //         ItemCell cell = itemCells.Find(t => t.GetItem().displayName == itemData.displayName);
        //         itemCells.Remove(cell);
        //         childCount --;//因为Destroy需要在这一帧结束才能生效，所以下方需要依照子物体数量的循环会有问题，这里手动调整子物体数量
        //
        //         if (!Application.isPlaying)
        //         {
        //             DestroyImmediate(cell.gameObject);
        //             continue;
        //         }
        //         Destroy(cell.gameObject);
        //     }
        //     
        //     SetIndicator(false, null);
        // }
        //
        // RefreshIndicator();//刷新指示器位置
        //

        #endregion
    }

    void RefreshIndicator()
    {
        // if (curItemData == null||curItemData.displayName==string.Empty)
        // {
        //     indicator.gameObject.SetActive(false);
        //     return;
        // }
        //
        // ItemCell cell = itemCells.Find(t => t.GetItem() == curItemData);
        //
        // if (cell == null)return;
        //
        // SetIndicator(true, cell.GetParent());
    }

    public void ChangeCurrentItem(ItemData itemData, ItemCell itemCell, Transform trans)
    {
        if (itemData == curItemData)
        {
            curItemData = null;
            curItemCell = null;
            //indicator.gameObject.SetActive(false);
            return;
        }

        curItemCell = itemCell;
        curItemData = itemData;
        SetIndicator(true, trans);
    }

    void SetIndicator(bool active, Transform trans)
    {
        // if (active)
        // {
        //     indicator.gameObject.SetActive(true);
        //     indicator.SetParent(trans);
        //     indicator.transform.localPosition = Vector3.zero;
        //     indicator.transform.rotation = trans.rotation;
        //     indicator.transform.localScale = trans.localScale;
        // }
        // else
        // {
        //     indicator.gameObject.SetActive(false);
        //     indicator.SetParent(trans); //这一行可能会导致指示器的缩放改变//但是没有这行可能导致指示器被删除
        // }
    }

    public ItemData[] testItems;

    [ContextMenu("测试添加")]
    public void TestAdd()
    {
        foreach (ItemData i in testItems)
        {
            AddItem(i);
        }

        foreach (var i in ItemDict)
        {
            Debug.Log(i);
        }
    }
    
    [ContextMenu("删除测试")]
    public void TestDestroy()
    {
        if (curItemData == null)
        {
            Debug.Log("当前没有选中物品");
        }

        DestroyItem(curItemData);
        foreach (var i in ItemDict)
        {
            Debug.Log(i);
        }
    }

    // private void OnGUI()
    // {
    //     #if UNITY_EDITOR
    //     GUILayout.BeginArea(new Rect(0,0,1000,1000));
    //     for (int i = 0; i < items.Count; i++)
    //     {
    //         GUILayout.Label($"{i + 1}: {items[i].displayName}");
    //     }
    //     GUILayout.EndArea();
    //     #endif
    // }
}