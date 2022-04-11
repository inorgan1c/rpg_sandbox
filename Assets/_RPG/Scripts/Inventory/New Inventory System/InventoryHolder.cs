using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private InventorySystemEventChannel inventorySystemEventChannel;
    [SerializeField] private int DefaultSlotsCount = 0;
    [SerializeField] private bool CanCreateSlots = false;

    private InventorySystem.Inventory _inventory = new InventorySystem.Inventory();
    public InventorySystem.Inventory Inventory => _inventory;

    private void Awake()
    {
        inventorySystemEventChannel.OnLootItem += OnLoot;
    }

    private void OnDestroy()
    {
        inventorySystemEventChannel.OnLootItem -= OnLoot;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<DefaultSlotsCount; ++i)
        {
            _inventory.CreateSlot();
        }

    }

    private void OnLoot(InventorySystem.InventoryItem item, int quantity)
    {
        InventorySystem.InventorySlot slot = _inventory.FindFirst(slot => slot.Item == item);

        if (slot == null && CanCreateSlots)
        {
            slot = _inventory.CreateSlot();
        }
        slot?.StoreItem(item, quantity);
    }

    private void OnUse(InventorySystem.InventoryItem item, int quantity)
    {
        InventorySystem.InventorySlot slot = _inventory.FindFirst(slot => slot.Item == item);

        if (slot == null && CanCreateSlots)
        {
            slot = _inventory.CreateSlot();
        }
        slot?.Use(quantity);
    }
}
