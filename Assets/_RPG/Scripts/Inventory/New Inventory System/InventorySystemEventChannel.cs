using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Inventory System Event Channel", menuName = "Event/Inventory System Event Channel")]
public class InventorySystemEventChannel : ScriptableObject
{
    public UnityAction<InventoryItem, int> OnLootItem;
    public UnityAction<InventoryItem, int> OnUseItem;

    public void RaiseLootItemEvent(InventoryItem item, int quantity)
    {
        OnLootItem?.Invoke(item, quantity);
    }

    public void RaiseLootItemEvent(InventoryItem item)
    {
        OnLootItem?.Invoke(item, 1);
    }

    public void RaiseUseItem(InventoryItem item, int quantity)
    {
        OnUseItem?.Invoke(item, quantity);
    }

    public void RaiseUseItem(InventoryItem item)
    {
        OnUseItem?.Invoke(item, 1);

    }


}
