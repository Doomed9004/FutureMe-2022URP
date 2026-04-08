using UnityEngine;

public class ForceAspectRatio : MonoBehaviour
{
      // 设置你期望的宽高比，例如16：9
        public float targetAspect = 16f / 9f;
    
        void Start()
        {
            // 获取当前屏幕的宽高比
            float windowAspect = (float)Screen.width / Screen.height;
            // 计算当前屏幕比例与目标比例的比例关系
            float scaleHeight = windowAspect / targetAspect;
            Camera cam = GetComponent<Camera>();
    
            // 如果当前屏幕宽高比大于目标比例（屏幕更宽）
            if (scaleHeight < 1.0f)
            {
                // 调整相机的 Viewport Rect，在左右两侧添加黑边
                Rect rect = cam.rect;
                rect.width = 1.0f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1.0f - scaleHeight) / 2.0f;
                cam.rect = rect;
            }
            else // 否则，屏幕更高或相等，在上下添加黑边
            {
                float scaleWidth = 1.0f / scaleHeight;
                Rect rect = cam.rect;
                rect.width = scaleWidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scaleWidth) / 2.0f;
                rect.y = 0;
                cam.rect = rect;
            }
        }
}
