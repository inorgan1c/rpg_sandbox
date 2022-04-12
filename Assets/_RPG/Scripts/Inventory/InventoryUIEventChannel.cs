using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New InventoryUI Event Channel", menuName = "Event/InventoryUI Event Channel")]
public class InventoryUIEventChannel : ScriptableObject
{
    public UnityAction<InventoryHolder> OnInventoryToggle;

    public void RaiseInventoryToggle(InventoryHolder inventoryHolder)
    {
        OnInventoryToggle?.Invoke(inventoryHolder);
    }

}
