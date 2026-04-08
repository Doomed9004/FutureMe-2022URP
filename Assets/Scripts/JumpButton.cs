using System;
using UnityEngine;
using UnityEngine.UI;

public class JumpButton : JumpBase
{
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=>Jump(scene));
    }
}
