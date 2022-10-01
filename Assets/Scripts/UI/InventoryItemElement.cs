using UnityEngine;
using UnityEngine.UI;

public class InventoryItemElement : MonoBehaviour
{
    [SerializeField] private Image _icon;

    public ItemSO currentItem;

    public void Initialize(ItemSO item)
    {
        currentItem = item;
        _icon.sprite = item.Image;
    }
}