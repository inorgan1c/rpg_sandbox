using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gather Goal", menuName = "Quests/Goals/Gather Goal")]

public class GatheringGoal : Quest.QuestGoal
{
    public Item item;

    public override string Description()
    {
        string descr = "Gather " + requiredAmount + " " + item.name;
        return descr;
    }

    public override void Initialize()
    {
        base.Initialize();
        Inventory.instance.OnItemChangedCallback += OnGathering;
    }

    private void OnGathering()
    {
        CurrentAmount = Inventory.instance.GetItemAmount(item);
        Evaluate();
    }
}
