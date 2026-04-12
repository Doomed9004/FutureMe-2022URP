using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    [System.Serializable]
    public class DialogList
    {
        public string[] texts;
        //[HideInInspector]
        //public Dialogue dialogue;
        public GameObject scene;
    }

    public SceneManager SceneManager;
    private DialogList curDL;
    public DialogWindow window;
    public TextMeshPro text;

    
    [SerializeField]DialogList[] dialogs;
    Dictionary<GameObject,DialogList> dialogsDict=new Dictionary<GameObject,DialogList>();
    Dictionary<DialogList,bool> dialogsState=new Dictionary<DialogList, bool>();//记录状态，第一次进入场景时主动弹出，再次进入时只能被动弹出
    private void Awake()
    {
        SceneManager.OnSceneChange += SceneChangeEventHandler;
        window.OnClick += ClickedEventHandler;
        foreach (var dl in dialogs)//初始化设置初始值
        {
            dialogsDict[dl.scene] = dl;
            dialogsState[dl] = false;
            // dl.dialogue.Initialize(dl.texts,text);
            // dl.dialogue.OnFinish += DialogFinishEventHandler;
        }
    }

    private void DialogFinishEventHandler()
    {
        //对话结束关闭窗口并重置Dialogue
        window.gameObject.SetActive(false);

        curDL = null;
        curText = "";
    }

    
    
    void SceneChangeEventHandler(GameObject scene)
    {
        //根据场景显示文案，改变状态
        //这个场景存在文案配置，并且首次访问
        if (dialogsDict.TryGetValue(scene, out DialogList dl)&&!dialogsState[dl])
        {
            dialogsState[dl] = true;
            //开始显示
            BeginText(dl);
            window.gameObject.SetActive(true);
        }
        else
        {
            //TODO：处理场景不存在或者退出场景的情况
        }
    }
    private void ClickedEventHandler()
    {
        if(curDL == null)return;
           TryNextText(curDL);
    }

    public float delay = .1f;
    private string curText;
    private int curIndex = 0;
    private bool isTextComplete = false; // 标记当前文本是否已完全显示
    private bool isTyping = false;        // 标记是否正在打字过程中
    
    Coroutine coroutine;
    public void TryNextText(DialogList dl)
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
            NextLine(dl);
        }
    }
    
    private void NextLine(DialogList dl)
    {
        curIndex++;
        if (curIndex < dl.texts.Length)
        {
            curText = dl.texts[curIndex];
            coroutine = StartCoroutine(ShowText(dl));
        }
        else
        {
            // 所有文本显示完毕，可添加结束逻辑
            Debug.Log("所有对话已结束");
            //TODO：关闭并重置对话
            
            //OnFinish?.Invoke();
        }
    }

    public void BeginText(DialogList dl)
    {
        curDL = dl;
        curIndex = 0;
        curText = dl.texts[curIndex];
        coroutine = StartCoroutine(ShowText(dl));
    }

    private IEnumerator ShowText(DialogList dl)
    {
        //显示第一个元素
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

}
