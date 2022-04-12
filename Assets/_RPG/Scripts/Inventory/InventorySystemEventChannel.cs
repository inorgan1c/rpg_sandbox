using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Inventory System Event Channel", menuName = "Event/Inventory System Event Channel")]
public class InventorySystemEventChannel : ScriptableObject
{
    public UnityAction<Item, int> OnLootItem;
    public UnityAction<Item, int> OnUseItem;

    public void RaiseLootItemEvent(Item item, int quantity)
    {
        OnLootItem?.Invoke(item, quantity);
    }

    public void RaiseLootItemEvent(Item item)
    {
        OnLootItem?.Invoke(item, 1);
    }

    public void RaiseUseItem(Item item, int quantity)
    {
        OnUseItem?.Invoke(item, quantity);
    }

    public void RaiseUseItem(Item item)
    {
        OnUseItem?.Invoke(item, 1);
    }
}
