using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewItem", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public AudioClip clickSound;
    public AudioClip useSound;
    //public bool canReuse;
}