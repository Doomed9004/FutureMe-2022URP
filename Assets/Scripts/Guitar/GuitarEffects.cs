using System.Collections;
using UnityEngine;

public class GuitarEffects : MonoBehaviour
{
    public string text;
    [Header("Generate Parameters")] 
    public float generateDuration = 0.5f;
    public GameObject prefab;
    public Transform generatePoint;

    [Header("Number Effects")]
    public Vector2 velocity;
    public float duration=2f;
    public float fadeTime=0.3f;

    Coroutine coroutine;
    public void StartGenerateEffect()
    {
        if (coroutine == null)
        {
            print("coroutine is null");
            coroutine = StartCoroutine(GenerateEffect());
        }
        else
        {
            print("coroutine is already running");
        }
    }
    IEnumerator GenerateEffect()
    {
        int index = 0;
        while (index < text.Length)
        {
            GameObject effect = Instantiate(prefab, generatePoint.position, generatePoint.rotation);
            effect.GetComponent<NumberEffect>().Init($"{text[index]}", velocity, duration, fadeTime);
            
            index++;
            yield return new WaitForSeconds(generateDuration);
        }
        
        coroutine=null;
    }
}
