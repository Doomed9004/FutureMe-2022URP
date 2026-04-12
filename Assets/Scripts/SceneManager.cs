using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private Transform root;
    //[SerializeField] CustomSceneData[] scenes;
    [SerializeField] private float duration;
    [SerializeField] CustomSceneData currentSceneData;
    public GameObject curScene;
    
    [System.Serializable]
    class CustomSceneData
    {
        public SpriteRenderer spriteRenderer;
        public GameObject obj;

        public CustomSceneData(GameObject obj, SpriteRenderer spriteRenderer)
        {
            this.obj = obj;
            this.spriteRenderer = spriteRenderer;
        }
    }
    
    Dictionary<GameObject,SpriteRenderer> scenesDict = new Dictionary<GameObject,SpriteRenderer>();
    public static SceneManager Ins{get; private set;}
    
    public event Action<GameObject> OnSceneChange;

    private void Awake()
    {
        //scenes = new CustomSceneData[root.childCount];
        for (int i = 0; i < root.childCount; i++)
        {
            Transform child = root.GetChild(i);
            GameObject obj = child.gameObject;
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            //scenes[i]=new CustomSceneData(obj,spriteRenderer);
            scenesDict[obj] = spriteRenderer;
            
            if(obj==currentSceneData.obj)continue;
            
            Color color = spriteRenderer.color;
            color=new Color(color.r, color.g, color.b, 0);
            spriteRenderer.color = color;
        }

        if (Ins == null)
            Ins = this;
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
            a=Mathf.Lerp(1,0, currentTime / duration);
            Color color = currentSceneData.spriteRenderer.color;
            color=new Color(color.r, color.g, color.b, a);
            currentSceneData.spriteRenderer.color = color;
            
            currentTime += Time.deltaTime;
            yield return null;
        }
        currentTime = 0;

        //切换页面
        // foreach (CustomSceneData go in scenes)
        // {
        //     go.obj.SetActive(go.obj == scene);
        // }

        foreach (var kv in scenesDict)
        {
            kv.Key.SetActive(kv.Key == scene);
        }
        
        //淡入
        while (currentTime < duration)
        {
            a=Mathf.Lerp(0,1, currentTime / duration);
            Color color = scenesDict[scene].color;
            color=new Color(color.r, color.g, color.b, a);
            scenesDict[scene].color = color;
            
            currentTime += Time.deltaTime;
            yield return null;
        }
        
        currentSceneData.obj=scene;
        currentSceneData.spriteRenderer = scenesDict[scene];
        OnSceneChange?.Invoke(scene);
    } 
}
