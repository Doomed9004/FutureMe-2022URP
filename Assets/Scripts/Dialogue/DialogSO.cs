using UnityEngine;

[CreateAssetMenu(fileName = "DialogSO", menuName = "DialogSO")]
public class DialogSO : ScriptableObject
{
    public DialogList[] dialogs;
}
[System.Serializable]
public class DialogList
{
    public string[] texts;//文本内容
    public GameObject scene;//目标场景

    public DialogList(string[] texts, GameObject scene)
    {
        this.texts = texts;
        this.scene = scene;
    }
}