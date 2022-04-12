using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name = "New Item";
    public Sprite Icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        Debug.Log("Using: " + Name);
    }
}
