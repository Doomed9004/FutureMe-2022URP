using UnityEngine;

public class ConditionPickup : MonoBehaviour,IInteractable
{
    [SerializeField]ItemData itemData;//需要使用的物品
    
    [SerializeField]ItemData rewardItem;//条件满足后获得的物品

    [SerializeField]bool consumeRequiredItem = true;//是否消耗条件物品
    [SerializeField]bool destroyAfterPickup = false;//使用后是否销毁自身
    
    [SerializeField]AudioClip successSound;
    [SerializeField]AudioClip failSound;
    [SerializeField] string failHint = "好像需要什么东西……";

    private bool isUsed=false;
    [SerializeField]bool canReuse=false;


    public void OnInteract(ItemData heldItem)
    {
        if (!canReuse)//不能重复使用的对象 如果被使用过了 那么返回
            if(isUsed)return;

        if (heldItem == itemData)
        {
            if(successSound)
                AudioSource.PlayClipAtPoint(successSound, transform.position);
            if(rewardItem)
                ItemManager.Ins.AddItem(rewardItem);

            if (consumeRequiredItem)//销毁条件物品
            {
                ItemManager.Ins.DestroyItem(heldItem);
            }

            if (destroyAfterPickup) //销毁自身
            {
                Destroy(gameObject);
            }
            
            UseItem();
            isUsed=true;
        }
        else
        {
            if(failSound)
                AudioSource.PlayClipAtPoint(failSound, transform.position);
            //TODO:展示失败信息
        }
    }

    virtual protected void UseItem()
    {
        //满足条件后发生什么
        
    }
}
