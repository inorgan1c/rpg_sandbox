using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    public int healthModifier = 20; 

    public override void Use()
    {
        base.Use();

        Transform player = PlayerManager.instance.player;
        CharacterStats stats = player.GetComponent<CharacterStats>();
        stats.Heal(healthModifier);
        RemoveFromInventory();
    }
}
