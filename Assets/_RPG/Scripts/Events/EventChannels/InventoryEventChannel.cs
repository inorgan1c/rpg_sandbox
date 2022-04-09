using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Inventory Event Channel", menuName = "Event/Inventory Event Channel")]
public class InventoryEventChannel : ScriptableObject
{
    public UnityAction OnInventoryUpdate;
    public UnityAction<Item> OnItemPickUp;


    public void RaiseInventoryUpdate()
    {
        OnInventoryUpdate?.Invoke();
    }
}
