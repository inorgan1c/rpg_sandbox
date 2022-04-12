using InventorySystem;
using UnityEngine;


[CreateAssetMenu(fileName = "New Gather Goal", menuName = "Quests/Goals/Gather Goal")]

public class GatheringGoal : Quest.QuestGoal
{
    public Item item;

    [SerializeField] InventorySystemEventChannel inventorySystemEventChannel;
    Inventory inventory;


    public override string Description()
    {
        string descr = "Gather " + requiredAmount + " " + item.Name;
        return descr;
    }

    public override void Initialize()
    {
        base.Initialize();
        inventorySystemEventChannel.OnLootItem += OnGathering;
        inventory = PlayerManager.instance.player.GetComponent<InventoryHolder>()?.Inventory;
    }

    private void OnGathering(Item lootItem, int quantity)
    {
        InventorySlot slot = inventory.FindFirst(s => (s.Item != null) && (s.Item == item));
        if (slot != null)
        {
            Debug.Log(slot.Item + " " + slot.Quantity);
            CurrentAmount = slot.Quantity;
            Evaluate();
        }
        else
        {
            CurrentAmount = 0;
        }
    }
}
