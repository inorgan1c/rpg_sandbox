using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    public int healthModifier = 20; 
    public int energyModifier = 20; 

    public override void Use(Inventory inventory = null)
    {
        base.Use();

        Transform player = PlayerManager.instance.player;
        CharacterStats stats = player.GetComponent<CharacterStats>();
        PlayerStats pstats = stats as PlayerStats;
        stats.Heal(healthModifier);
        pstats.RestoreEnergy(energyModifier);
        RemoveFromInventory(inventory);
    }
}
