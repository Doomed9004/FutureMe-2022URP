using System;
using UnityEngine;

public class MultiPassword : MonoBehaviour
{
    public SinglePassword[] passwords;
    public GuitarPassword[] answer;

    public event Action Unlock;
    
    private void Awake()
    {
        foreach (SinglePassword sp in passwords)
        {
            sp.OnChange += () => Check();
        }
        SetActive(false);
    }

    public void SetActive(bool b)
    {
        foreach (SinglePassword sp in passwords)
        {
            sp.SetCanInteract(b);
        }
    }
    bool Check()
    {
        for (int i = 0; i < passwords.Length; i++)
        {
            if (passwords[i].GetPassword() != answer[i]) return false;
        }
        Unlock?.Invoke();
        Debug.Log("Unlocked!");
        return true;
    }
}
