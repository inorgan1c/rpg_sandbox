using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EquipmentSlot { Head, Chest, Legs, Feet, Weapon, Shield }
public enum EquipmentMeshRegion { Legs, Arms, Torso } //Body blendshapes


[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public GameObject meshPrefab;
    public EquipmentMeshRegion[] coveredMeshRegion;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();

        EquipmentManager equipmentManager = PlayerManager.instance.GetComponent<EquipmentManager>();
        
        if (equipmentManager)
        {
            equipmentManager.Equip(this);
        
        } else
        {
            Debug.Log("Cannot equip: no equipment manager component");
        }
        
    }

    public SkinnedMeshRenderer Mesh()
    {
        return meshPrefab.GetComponent<SkinnedMeshRenderer>();
    }
}
