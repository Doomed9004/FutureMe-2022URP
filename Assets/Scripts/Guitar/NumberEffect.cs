using System;
using System.Collections;
using UnityEngine;
using TMPro;
public class NumberEffect : MonoBehaviour
{
    public Vector2 velocity;
    public float duration=2f;
    public float fadeTime=0.3f;
    private TextMeshPro _tmp;

    public void Init(string text, Vector2 velocity, float duration, float fadeTime)
    {
        _tmp = GetComponent<TextMeshPro>();
        
        _tmp.text = text;
        this.velocity = velocity;
        this.duration = duration;
        this.fadeTime = fadeTime;
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        float timer = 0;
        while (timer <= fadeTime)
        {
            timer += Time.deltaTime;
            transform.Translate(velocity * Time.deltaTime);
            _tmp.color = new Color(0f ,0f, 0f, timer / fadeTime);
            
            yield return null;
        }

        timer = 0;
        
        while (timer <= duration)
        {
            timer += Time.deltaTime;
            transform.Translate(velocity * Time.deltaTime);
            
            yield return null;
        }
        timer = 0;
        
        while (timer <= fadeTime)
        {
            timer += Time.deltaTime;
            transform.Translate(velocity * Time.deltaTime);
            _tmp.color = new Color(0f ,0f, 0f, 1-(timer / fadeTime));
            
            yield return null;
        }
        Destroy(gameObject);
    }
}
