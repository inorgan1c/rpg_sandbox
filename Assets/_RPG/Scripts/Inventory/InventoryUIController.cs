using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] GameObject inventoryUIPanel;
    [SerializeField] private InventoryUIEventChannel inventoryUIEventChannel;
    [SerializeField] private InventorySlotUIController slotUIPrefab;

    Inventory _displayedInventory;
    public Inventory DisplayedInventory => _displayedInventory;

    private void Awake()
    {
        inventoryUIEventChannel.OnInventoryToggle += OnInventoryToggle;
        if (!inventoryUIPanel)
        {
            inventoryUIPanel = gameObject;
        }
        inventoryUIPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        inventoryUIEventChannel.OnInventoryToggle -= OnInventoryToggle;
    }

    private void OnInventoryToggle(InventoryHolder inventoryHolder)
    {
        if (_displayedInventory == null)
        {
            inventoryUIPanel.SetActive(true);
            _displayedInventory = inventoryHolder.Inventory;
            _displayedInventory.OnSlotAdded += CreateSlotController;
            _displayedInventory.OnSlotRemoved += DestroySlotController;
            _displayedInventory.ForEach(CreateSlotController);
        
        } else if (_displayedInventory == inventoryHolder.Inventory)
        {
            inventoryUIPanel.SetActive(false);
            _displayedInventory.OnSlotAdded -= CreateSlotController;
            _displayedInventory.OnSlotRemoved -= DestroySlotController;
            _displayedInventory = null;
            foreach (InventorySlotUIController slot in 
                GetComponentsInChildren<InventorySlotUIController>()) {
                
                Destroy(slot.gameObject); 
            };
        }
    }

    private void CreateSlotController(InventorySlot slot)
    {
        InventorySlotUIController slotUIController = Instantiate(slotUIPrefab, transform);
        slotUIController.Slot = slot;
    }

    private void DestroySlotController(InventorySlot slot)
    {
        InventorySlotUIController[] slots = GetComponentsInChildren<InventorySlotUIController>();
        InventorySlotUIController found = Array.Find(slots, s => slot.Item == s.Slot.Item);

        if (found)
        {
            Destroy(found.gameObject);
        }
    }
}
