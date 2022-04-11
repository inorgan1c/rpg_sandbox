using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Inventory Item")]
    public class InventoryItem : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
    }
}

