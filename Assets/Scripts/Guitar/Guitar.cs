using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Guitar : MonoBehaviour,IPointerUpHandler
{
    private GuitarState curState = GuitarState.Misshapen;
    public ItemData misshapenItem;
    public MultiPassword multiPassword;
    bool isTuned = false;

    public Sprite guitarSprite;
    
    public GuitarEffects effects;
    public AudioSource audioSource;
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
        multiPassword.SetActive(false);
        curState = GuitarState.Complete;
        //TODO:提示调弦完成
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (curState)
        {
            case GuitarState.Misshapen:
                if (misshapenItem!=null && ItemManager.Ins.curItemData == misshapenItem)
                {
                    //TODO:上弦，显示调弦界面
                    multiPassword.SetActive(true);//显示条线页面
                    GetComponent<SpriteRenderer>().sprite = guitarSprite;//更换上线后的素材
                    curState = GuitarState.NotTuned;
                    
                    audioSource.Play();
                    //移除道具
                    ItemManager.Ins.DestroyItem();
                }
                break;
            case GuitarState.Complete:
                //TODO:展示提示信息
                effects.StartGenerateEffect();
                break;
        }
    }

    [ContextMenu("特效测试")]
    public void EffectTest()
    {
        effects.StartGenerateEffect();
    }
}
