using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private InventorySystemEventChannel inventorySystemEventChannel;
    [SerializeField] private int DefaultSlotsCount = 0;
    [SerializeField] private bool CanCreateSlots = false;

    private Inventory _inventory = new Inventory();
    public Inventory Inventory => _inventory;

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

    private void OnLoot(Item item, int quantity)
    {
        InventorySlot slot = _inventory.FindFirst(slot => slot.Item == item || slot.Item == null);
        if (slot == null && CanCreateSlots)
        {
            slot = _inventory.CreateSlot();
        }

        slot?.StoreItem(item, quantity);
    }
}
