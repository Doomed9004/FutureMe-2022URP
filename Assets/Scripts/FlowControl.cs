using System;
using UnityEngine;

public class FlowControl : MonoBehaviour
{
    public SceneManager sceneManager;
    [Header("Child")]
    public GameObject course;
    public GameObject bed;
    public GameObject character12,character23;

    [Header("Youth")] 
    public EmptyPage emptyPage;
    public GameObject bed2;

    public event Action OnAwake;
    // [Header("Awake")]
    enum Chapters
    {
        Child,
        Youth,
        Awake
    }
    
    private void OnEnable()
    {
        sceneManager.OnSceneChange += Func;
        emptyPage.SubmitInput += Func2;
    }
    
    private void OnDisable()
    {
        sceneManager.OnSceneChange -= Func;
        emptyPage.SubmitInput -= Func2;
    }

    Chapters chapter=Chapters.Child;

    void Func2()
    {
        bed2.SetActive(true);
        chapter=Chapters.Awake;
        OnAwake?.Invoke();
    }
    void Func(GameObject scene)
    {
        switch (chapter)
        {
            case Chapters.Child:
                if (scene.name == "日记12")
                {
                    course.SetActive(false);
                    bed.SetActive(true);
                }

                if (scene.name == "主场景23")
                {
                    character12.SetActive(false);
                    character23.SetActive(true);
                    
                    chapter = Chapters.Youth;
                }
                
                break;
            case Chapters.Youth:
                
                break;
            case Chapters.Awake:
                
                break;
        }
    }
}
