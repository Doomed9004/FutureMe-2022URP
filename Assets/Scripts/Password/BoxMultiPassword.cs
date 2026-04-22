using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxMultiPassword : MonoBehaviour
{
    public List<BoxSinglePassword> passwords=new List<BoxSinglePassword>();
    public int[] answer;
    
    public GameObject closeBox;
    public GameObject openBox;

    public event Action Unlock;

    private void OnEnable()
    {
        Unlock += UnlockEventHandler;
    }

    private void OnDisable()
    {
        Unlock -= UnlockEventHandler;
    }

    private void Awake()
    {
        foreach (BoxSinglePassword bsp in passwords)
        {
            bsp.OnChange+= () => Check();
        }
        closeBox.SetActive(true);
        openBox.SetActive(false);
    }
    
    bool Check()
    {
        for (int i = 0; i < passwords.Count; i++)
        {
            if (passwords[i].GetPassword() != answer[i]) return false;
        }
        Unlock?.Invoke();
        
        return true;
    }

    public void UnlockEventHandler()
    {
        Debug.Log("Unlocked!");
        closeBox.SetActive(false);
        openBox.SetActive(true);
    }
}
