using System;
using UnityEngine;

public class CourseBlock : MonoBehaviour
{
    public GameObject target;

    private void Update()
    {
        if (target.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
