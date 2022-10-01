using UnityEngine;

//context menu for creating new Scriptable Object
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Image;
}