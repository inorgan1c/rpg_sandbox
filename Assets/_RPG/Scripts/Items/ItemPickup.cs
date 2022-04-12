using UnityEngine;
using InventorySystem;

public class ItemPickup : Interactable
{
    public Item item;
    [SerializeField] InventorySystemEventChannel inventorySystemEventChannel;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        inventorySystemEventChannel.RaiseLootItemEvent(item);
        Destroy(gameObject);
    }
}
