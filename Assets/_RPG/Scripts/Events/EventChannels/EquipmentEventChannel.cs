using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Equipment Event Channel", menuName = "Event/Equipment Event Channel")]
public class EquipmentEventChannel : ScriptableObject
{
    public UnityAction<Equipment, Equipment> OnEquipmentChanged;

    public void RaiseEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        OnEquipmentChanged?.Invoke(newItem, oldItem);
    }
}
