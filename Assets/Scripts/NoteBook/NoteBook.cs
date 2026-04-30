using System;
using UnityEngine;

public class NoteBook : MonoBehaviour
{
    private bool _canWrite;
    public GameObject button;

    private void Start()
    {
        button.SetActive(false);
    }

    public void SetCanWrite(bool b)
    {
        Debug.Log(b);
        _canWrite = b;
        if (!_canWrite)return;
        button.SetActive(true);
    }
}
