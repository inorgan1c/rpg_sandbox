using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject uiParent;
    public Transform slotsParent;

    [SerializeField] InventoryEventChannel inventoryEventChannel;
    Inventory inventory;
    InventorySlot[] slots;

    private void OnEnable()
    {
        inventoryEventChannel.OnInventoryUpdate += UpdateUI;
    }

    private void OnDisable()
    {
        inventoryEventChannel.OnInventoryUpdate -= UpdateUI;
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        slots = slotsParent.GetComponentsInChildren<InventorySlot>();
        uiParent.SetActive(false);
    }
     
    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            uiParent.SetActive(!uiParent.activeSelf);
        }
    }


    void UpdateUI()
    {
        Debug.Log("Update UI");
        for (int i=0; i < slots.Length; ++i)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }


}
