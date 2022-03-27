using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        bool pickedUp = Inventory.instance.AddItem(item);
        if (pickedUp)
        {
            Debug.Log("PickUp: " + item.name);
            Destroy(gameObject);
        }
    }
}
