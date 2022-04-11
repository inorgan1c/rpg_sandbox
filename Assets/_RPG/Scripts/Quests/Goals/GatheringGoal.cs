using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gather Goal", menuName = "Quests/Goals/Gather Goal")]

public class GatheringGoal : Quest.QuestGoal
{
    public Item item;
    
    [SerializeField] InventoryEventChannel inventoryEventChannel;
    Inventory inventory;


    public override string Description()
    {
        string descr = "Gather " + requiredAmount + " " + item.name;
        return descr;
    }

    public override void Initialize()
    {
        base.Initialize();
        inventoryEventChannel.OnInventoryUpdate += OnGathering;
        inventory = PlayerManager.instance.inventory;
    }

    private void OnGathering()
    {
        CurrentAmount = inventory.GetItemAmount(item);
        Evaluate();
    }
}
