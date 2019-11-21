using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryItem
{
    StoreItem reference;
    int item_quantity;
    public bool isEmpty;

    public InventoryItem(StoreItem item, int quantity)
    {
        isEmpty = false;
        reference = item;
        item_quantity = quantity;
    }

    public int GetQuantity()
    {
        return item_quantity;
    }

    public void IncrementQuantity()
    {
        item_quantity++;
    }

    public int DecreaseQuantity()
    {
        return --item_quantity;
    }

    public string GetName()
    {
        return reference.name;
    }

    public void Clear()
    {
        isEmpty = true;
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }
}