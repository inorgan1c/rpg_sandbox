using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUIController : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _removeBtn;
    [SerializeField] private Text _quantityText;

    private InventorySystem.InventorySlot _slot;
    public InventorySystem.InventorySlot Slot
    {
        get { return _slot; }
        set
        {
            if (_slot != null)
            {
                _slot.OnItemChange -= UpdateSlot;
            }

            _slot = value;
            UpdateSlot();

            if (_slot != null)
            {
                _slot.OnItemChange += UpdateSlot;
            }
        }
    }

    private void OnDestroy()
    {
        _slot = null;
    }

    public void DestroySlot()
    {
        InventoryUIController inventory = GetComponentInParent<InventoryUIController>();
        if (inventory != null)
        {
            inventory.DisplayedInventory.DestroySlot(_slot);
        }
    }

    public void Use()
    {
        if (_slot != null)
        {
            _slot.Use();
        }
    }

    private void UpdateSlot()
    {
        bool display = _slot != null && _slot.Item != null && _slot.Quantity > 0;
        if (_slot.Item)
        {
            Debug.Log(_slot.Item.name);
        }

        if (_icon != null)
        {
            _icon.gameObject.SetActive(display);
            _icon.sprite = display ? _slot.Item.Icon : null;
        }

        if (_quantityText != null)
        {
            _quantityText.gameObject.SetActive(display);
            _quantityText.text = display ?_slot.Quantity.ToString() : "";

        }

        if (_removeBtn != null)
        {
            _removeBtn.gameObject.SetActive(display);
        }
    }
}
