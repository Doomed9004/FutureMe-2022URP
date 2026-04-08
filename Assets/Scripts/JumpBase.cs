using UnityEngine;

public class JumpBase : MonoBehaviour
{
    public GameObject scene;

    protected void Jump(GameObject scene)
    {
        SceneManager.Ins.ChangeScene(scene);
    }
}
