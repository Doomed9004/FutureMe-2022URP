using UnityEngine;

public class UnlockSomething : MonoBehaviour
{
    public GameObject unlockObj;
    public void Unlock()
    {
        unlockObj.SetActive(true);
    }
}
