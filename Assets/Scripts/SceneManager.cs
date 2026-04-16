using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private float duration;

    public GameObject curScene;
	public SpriteRenderer blackBoard;
    private GameObject[] _scenes;
    
    public static SceneManager Ins{get; private set;}
    public event Action<GameObject> OnSceneChange;

    private void Awake()
    {
        if (Ins == null)
            Ins = this;
        
        _scenes=new GameObject[root.childCount];
        for (int i = 0; i < root.childCount; i++)
        {
            Transform child = root.GetChild(i);
            _scenes[i] = child.gameObject;
        }
    }
    
    public void ChangeScene(GameObject scene)
    {
        StartCoroutine(Transition(scene));
    }

    IEnumerator Transition(GameObject scene)
    {
        float currentTime = 0;
        float a = 1;
        //淡出
        while (currentTime < duration)
        {
            a=Mathf.Lerp(0,1, currentTime / duration);
            Color color = new Color(0, 0, 0, a);
            blackBoard.color=color;
            
            currentTime += Time.deltaTime;
            yield return null;
        }
        currentTime = 0;

        foreach (var i in _scenes)
        {
            i.SetActive(i == scene);
        }
        
        //淡入
        while (currentTime < duration)
        {
            a=Mathf.Lerp(1,0, currentTime / duration);
            Color color = new Color(0, 0, 0, a);
            blackBoard.color=color;
            
            currentTime += Time.deltaTime;
            yield return null;
        }
        
        curScene = scene;
        OnSceneChange?.Invoke(scene);
    } 
}
