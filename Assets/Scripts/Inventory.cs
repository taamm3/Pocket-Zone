using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> _items = new List<Item>();
    [SerializeField] private Transform UIContent;
    [SerializeField] private InventoryItem inventoryItemPrefab;

    public void AddItem(Item item)
    {
        if (!Contains(item))
        {
            _items.Add(item);
            InventoryItem invItem = Instantiate(inventoryItemPrefab, UIContent);
            invItem.Init(item);
            _items[^1].InventoryItem = invItem;
        }
    }

    private bool Contains(Item item)
    {
        foreach (Item i in _items)
        {
            if (item.ItemID == i.ItemID)
            {
                i.Count++;
                i.InventoryItem.UpdateCount(i.Count);
                return true;
            }
        }
        return false;
    }

    public void DeleteItem(int id)
    {
        Item itemToRemove = _items.Find(i => i.ItemID == id);
        _items.Remove(itemToRemove);
    }
}
