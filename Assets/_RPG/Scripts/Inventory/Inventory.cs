using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory
{
    private readonly List<InventorySlot> slots = new List<InventorySlot>();

    public UnityAction<InventorySlot> OnSlotAdded;
    public UnityAction<InventorySlot> OnSlotRemoved;


    public InventorySlot CreateSlot()
    {
        InventorySlot slot = new InventorySlot();
        slots.Add(slot);

        OnSlotAdded?.Invoke(slot);

        return slot;
    }

        
    public void DestroySlot(InventorySlot slot)
    {
        slots.Remove(slot);

        OnSlotRemoved?.Invoke(slot);
    }


    public void Clear()
    {
        slots.ForEach(slot => DestroySlot(slot));
    }

    public void ForEach(Action<InventorySlot> action)
    {
        slots.ForEach(slot => action.Invoke(slot));
    }

    public InventorySlot FindFirst(Predicate<InventorySlot> predicate)
    {
        return slots.Find(predicate);
    }

    public List<InventorySlot> FindAll(Predicate<InventorySlot> predicate)
    {
        return slots.FindAll(predicate);
    }

}

