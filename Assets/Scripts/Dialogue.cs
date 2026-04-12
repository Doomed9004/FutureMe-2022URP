using System;
using System.Collections;
using TMPro;
using UnityEngine;

[Serializable]
public class Dialogue : MonoBehaviour
{
    [Header("文字显示")]
    public string[] texts;
    public TextMeshPro text;
    public float delay = 0.1f;
    [Header("过渡")]
    //public Button button;
    // public CanvasGroup canvasGroup;
    // public float duration = 1.6f;

    private bool canInteractive;
    private string curText;
    private int curIndex = 0;
    private bool isTextComplete = false; // 标记当前文本是否已完全显示
    private bool isTyping = false;        // 标记是否正在打字过程中

    public event Action OnFinish;

    public void Initialize(string[] content,TextMeshPro text)
    {
        texts = content;
        this.text = text;
    }
    private void Start()
    {
        curText=texts[0];
        text.text = "";
        // button.onClick.AddListener(() => {StartCoroutine(Transform());});
        // button.gameObject.SetActive(false);
    }
    
    public bool IsAllFinish()
    {
        return isTextComplete && curIndex==texts.Length-1;
    }

    Coroutine coroutine;
    public void TryNextText()
    {
        if (isTyping)
        {
            // 如果正在打字过程中，立即完成当前文本显示
            if(coroutine!=null)
                StopCoroutine(coroutine);
            text.text = curText;
            isTextComplete = true;
            isTyping = false;
        }
        else if (isTextComplete)
        {
            // 如果文本已完全显示，切换到下一段
            NextLine();
        }
    }
    
    private void NextLine()
    {
        curIndex++;
        if (curIndex < texts.Length)
        {
            curText = texts[curIndex];
            coroutine = StartCoroutine(ShowText());
        }
        else
        {
            // 所有文本显示完毕，可添加结束逻辑
            Debug.Log("所有对话已结束");
            //TODO：关闭并重置对话
            OnFinish?.Invoke();
        }
    }

    public void BeginText()
    {
        canInteractive = true;
        coroutine = StartCoroutine(ShowText());
    }

    public void Reset()
    {
        curIndex = 0;
    }

    private IEnumerator ShowText()
    {
        isTyping = true;
        isTextComplete = false;
        text.text = "";

        foreach (char c in curText)
        {
            text.text += c;
            yield return new WaitForSeconds(delay); // 调整打字速度
        }

        isTextComplete = true;
        isTyping = false;
    }
    
    // IEnumerator Transform()
    // {
    //     float timer = 0;
    //     while (timer < duration)
    //     {
    //         timer += Time.deltaTime;
    //         canvasGroup.alpha = 1-timer/duration;
    //         yield return null;
    //     }
    //     canvasGroup.alpha = 0;
    //     gameObject.SetActive(false);
    // }
}

