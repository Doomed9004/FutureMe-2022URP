using System;
using UnityEngine;

public class Brush : MonoBehaviour
{
    LineRenderer lineRenderer;
    private bool canDraw;
    private Vector3 mousePos;
    private Vector3 newMousePos;
    Camera mainCamera;
    
    
    public float drawPlaneZ = 0f;   // 你的画笔平面 Z 坐标（LineRenderer 所在平面）
    private Vector3 lastMouseWorldPos;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        mainCamera = Camera.main;
        //临时调试
        canDraw = true;
    }

    private void Update()
    {
        // newMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // if (canDraw&&newMousePos!= mousePos)
        // {
        //     Vector3 vec=new Vector3(newMousePos.x,newMousePos.y,0);//这里z轴大小会影响笔刷位置
        //     lineRenderer.positionCount++;
        //     int index=lineRenderer.positionCount-1;
        //     lineRenderer.SetPosition(index,vec);
        //     
        //     Debug.Log(vec);
        // }
        // mousePos=mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (!canDraw) return;

        if (Input.GetMouseButton(0))
        {
            // 1. 获取屏幕坐标，设置正确的 Z 值（相机空间深度）
            Vector3 mouseScreen = Input.mousePosition;
            mouseScreen.z = GetCameraDistanceToPlane();  // 关键修正
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mouseScreen);
            worldPos.z = drawPlaneZ;   // 强制设为你需要的平面（保险）

            // 2. 防止每帧重复添加相同点（距离太近时不加）
            if (lineRenderer.positionCount == 0 || Vector3.Distance(worldPos, lastMouseWorldPos) > 0.01f)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPos);
                lastMouseWorldPos = worldPos;
            }
        }
    }
    private float GetCameraDistanceToPlane()
    {
        // 计算相机到绘制平面的距离（绝对值）
        return Mathf.Abs(mainCamera.transform.position.z - drawPlaneZ);
    }

    private void OnDrawGizmos()
    {
        Vector3 vec=new Vector3(newMousePos.x,newMousePos.y,0);
        Gizmos.DrawCube(vec,Vector3.one);
    }
}

