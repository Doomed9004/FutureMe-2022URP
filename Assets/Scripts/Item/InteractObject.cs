using System;
using System.Collections;
using UnityEngine;

public class InteractObject : MonoBehaviour,IInteractable
{
    public AnimationClip openClip,closeClip;
    Animator animator;
    protected bool opened = false;
    protected bool isPlaying = false;
    
    public AudioClip open,close;

    protected event Action OnAniPlay;
    protected event Action OnAniFinish;

    virtual protected void Awake()
    {
        if(animator == null) animator = GetComponent<Animator>();
    }

    public void OnInteract(ItemData heldItem)
    {
        if (!isPlaying)
        {
            string clipName = opened ? closeClip.name : openClip.name;

            PlayAudioClip();
            
            isPlaying=true;
            
            StartCoroutine(PlayAnimation(clipName));
            DoSomething();
        }
    }

    virtual protected void DoSomething()
    {
        
    }

    virtual protected void PlayAudioClip()
    {
        if(!opened && open)
            AudioSource.PlayClipAtPoint(open, transform.position);
        if(opened && close)
            AudioSource.PlayClipAtPoint(close, transform.position);
    }
    
    

    IEnumerator PlayAnimation(string clipName)
    {
        isPlaying = true;
        animator.Play(clipName);
        OnAniPlay?.Invoke();

        while (true)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            // 检查当前是否在播放目标动画
            if (stateInfo.IsName(clipName))
            {
                // 检查动画是否播放完毕（normalizedTime >= 1）
                if (stateInfo.normalizedTime >= 1.0f)
                {
                    isPlaying = false;
                    Debug.Log($"动画播放完成: {clipName}");
                    OnAniFinish?.Invoke();
                    opened = !opened;
                    yield break;
                }
            }
            yield return null;
        }
        
    }
}
