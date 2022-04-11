using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;
    public Button removeBtn;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.Icon;
        icon.enabled = true;
        removeBtn.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeBtn.interactable = false;
    }

    public void OnRemoveButton(Inventory inventory=null)
    {
        if (!inventory)
        {
            inventory = PlayerManager.instance.inventory;
        }
        inventory.RemoveItem(item);
    }

    public void UseItem()
    {
        if (item)
        {
            item.Use();
        }
    }
}
