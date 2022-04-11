using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp(Inventory inventory = null)
    {
        bool pickedUp = false;

        if (!inventory)
        {
            inventory = PlayerManager.instance.inventory;
        }

        pickedUp = inventory.AddItem(item);
        if (pickedUp)
        {
            Debug.Log("PickUp: " + item.Name);
            Destroy(gameObject);
        }
    }
}
