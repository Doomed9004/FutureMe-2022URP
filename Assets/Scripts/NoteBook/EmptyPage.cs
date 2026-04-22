using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmptyPage : MonoBehaviour
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
