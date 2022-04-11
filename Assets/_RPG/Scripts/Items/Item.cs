using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name = "New Item";
    public Sprite Icon = null;
    public bool isDefaultItem = false;

    public virtual void Use(Inventory inventory = null)
    {
        Debug.Log("Using: " + Name);
    }

    public void RemoveFromInventory(Inventory inventory = null)
    {
        if (!inventory)
        {
            inventory = PlayerManager.instance.inventory;
        }

        inventory.RemoveItem(this);
    }
}
