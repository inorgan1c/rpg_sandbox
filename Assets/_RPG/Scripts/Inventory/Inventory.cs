using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int space = 20;
    public GameObject inventoryUI;

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

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
                if (OnItemChangedCallback != null)
                {
                    OnItemChangedCallback.Invoke();
                }
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
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
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

    private void Start()
    {
        inventoryUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}
