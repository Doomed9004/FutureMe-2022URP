using System;
using UnityEngine;

public class Brush : MonoBehaviour
{
    LineRenderer lineRenderer;
    private Vector3 lastMouseWorldPos;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    public void Draw(Vector3 pos,float depth)
    {
        pos.z = depth;

        // 防止每帧重复添加相同点（距离太近时不加）
        if (lineRenderer.positionCount == 0 || Vector3.Distance(pos, lastMouseWorldPos) > 0.01f)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
            lastMouseWorldPos = pos;
        }
    }
}

