using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int space = 20;

    [SerializeField] InventoryEventChannel inventoryEventChannel;

    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion


    public bool AddItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count < space)
            {
                items.Add(item);
                inventoryEventChannel?.RaiseInventoryUpdate();

            } else
            {
                Debug.Log("Not enough space in inventory");
                return false;
            }
        }
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        inventoryEventChannel?.RaiseInventoryUpdate();
    }

    public int GetItemAmount(Item item)
    {
        if (item)
        {
            List<Item> instances = items.FindAll(i => i.name == item.name);
            return instances.Count;
        } else
        {
            return 0;
        }
        
    } 
}
