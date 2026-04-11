using UnityEngine;

public class DrawOn3D : MonoBehaviour
{
    public RenderTexture drawCanvas;       // 画板
    public Texture brushTexture;           // 笔刷
    public Texture blankTexture;           // 空白图，用于清空画板
    private Camera mainCamera;             // 主摄像机

    void Start()
    {
        mainCamera = Camera.main;
        ClearCanvas();                     // 初始化时清空画板
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // 1. 从鼠标位置发射射线
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 2. 计算UV坐标，并换算为RenderTexture上的像素坐标
                Vector2 uv = hit.textureCoord;
                int x = (int)(uv.x * drawCanvas.width);
                int y = (int)(uv.y * drawCanvas.height);
                
                // 3. 执行绘制
                DrawAt(x, y);
            }
        }
    }

    void DrawAt(int x, int y)
    {
        // 核心绘制逻辑：通过GL矩阵操作，将笔刷贴图画到RenderTexture上
        RenderTexture.active = drawCanvas;
        GL.PushMatrix();
        // 设置正交投影矩阵，使坐标与像素坐标对齐
        GL.LoadPixelMatrix(0, drawCanvas.width, drawCanvas.height, 0);
        // 定义笔刷绘制区域
        Rect brushRect = new Rect(x - brushTexture.width / 2, y - brushTexture.height / 2, brushTexture.width, brushTexture.height);
        Graphics.DrawTexture(brushRect, brushTexture);
        GL.PopMatrix();
        RenderTexture.active = null;

		Debug.Log("Draw");
    }

    public void ClearCanvas()
    {
        RenderTexture.active = drawCanvas;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, drawCanvas.width, drawCanvas.height, 0);
        Graphics.DrawTexture(new Rect(0, 0, drawCanvas.width, drawCanvas.height), blankTexture);
        GL.PopMatrix();
        RenderTexture.active = null;
    }
}
