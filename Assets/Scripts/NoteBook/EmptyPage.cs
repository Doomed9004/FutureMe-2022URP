using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmptyPage : MonoBehaviour//场景中的emptyPage有神秘bug，一定以激活状态运行，如果失活状态运行会导致切换页面时页面丢失
{
    public TMP_InputField inputField;
    public Button button;
    
    public DialogBox dialogBox;
    public DialogList newDialog;

    public TextMeshPro tmp;

    public event Action SubmitInput;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SubmitInput?.Invoke();
        button.gameObject.SetActive(false);
        inputField.interactable = false;
        
        dialogBox.ShowDialog(newDialog);
        tmp.text=inputField.text;
    }

    [ContextMenu("条件达成测试")]
    public void Test()
    {
        OnClick();
    }
}
