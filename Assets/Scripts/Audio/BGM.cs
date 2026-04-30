using System;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public GameObject normalBGM;
    public GameObject dayBGM;
    public FlowControl flowControl;
    private void Awake()
    {
        flowControl=FindObjectOfType<FlowControl>();
        normalBGM.SetActive(true);
        dayBGM.SetActive(false);
    }

    private void OnEnable()
    {
        flowControl.OnAwake+=HandleOnAwake;
    }

    private void OnDisable()
    {
        flowControl.OnAwake-=HandleOnAwake;
    }

    void HandleOnAwake()
    {
        
    }
}
