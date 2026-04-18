using System;
using UnityEngine;

public class BoxMultiPassword : MonoBehaviour
{
    public BoxSinglePassword[] passwords;
    public int[] answer;

    public event Action Unlock;
    
    private void Awake()
    {
        foreach (BoxSinglePassword sp in passwords)
        {
            sp.OnChange += () => Check();
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
        gameObject.SetActive(false);
        return true;
    }
}
