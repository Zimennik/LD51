using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemSO> AllItems;
    [SerializeField] private RectTransform _itemHoder;
    [SerializeField] private InventoryItemElement _itemPrefab;

    public List<ItemSO> CurrentItems = new List<ItemSO>();

    public void ResetInventory()
    {
        //remove all items from the inventory
        foreach (Transform child in _itemHoder)
        {
            Destroy(child.gameObject);
        }
    }


    public void AddItemByName(string name)
    {
        var newItem = AllItems.FirstOrDefault(x => x.Name == name);

        CurrentItems.Add(newItem);

        //add the item to the inventory
        InventoryItemElement item = Instantiate(_itemPrefab, _itemHoder);
        item.Initialize(newItem);
    }

    public void RemoveItemByName(string name)
    {
        CurrentItems.Remove(AllItems.FirstOrDefault(x => x.Name == name));

        //remove the item from the inventory
        foreach (Transform child in _itemHoder)
        {
            if (child.GetComponent<InventoryItemElement>().currentItem.Name == name)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public bool HasItem(ItemSO item)
    {
        return CurrentItems.Contains(item);
    }
}