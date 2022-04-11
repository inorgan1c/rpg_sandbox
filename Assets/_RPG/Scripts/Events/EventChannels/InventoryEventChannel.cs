using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Inventory Event Channel", menuName = "Event/Inventory Event Channel")]
public class InventoryEventChannel : ScriptableObject
{
    public UnityAction OnInventoryUpdate;
    public UnityAction<Equipment, Equipment> OnEquipmentChanged;

    public void RaiseInventoryUpdate()
    {
        OnInventoryUpdate?.Invoke();
    }

    public void RaiseEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        OnEquipmentChanged?.Invoke(newItem, oldItem);
    }
}
