using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public SkinnedMeshRenderer targetMesh;
    public Equipment[] defaultItems;

    Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;
    [SerializeField] EquipmentEventChannel equipmentEventChannel;
    [SerializeField] InventorySystemEventChannel inventorySystemEventChannel;


    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
    }

    private void EquipDefaultItems()
    {
        foreach (Equipment equipment in defaultItems)
        {
            Equip(equipment);
        }
    }

    public void Equip(Equipment newItem)
    {
        int slotIdx = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIdx);

        if (oldItem != null && newItem != oldItem && !oldItem.isDefaultItem)
        {
            inventorySystemEventChannel?.RaiseLootItemEvent(oldItem);
        }

        equipmentEventChannel?.RaiseEquipmentChanged(newItem, oldItem);
        
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.Mesh());
        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        SetEquipmentBlendShapes(newItem, 100);

        currentMeshes[slotIdx] = newMesh;
        currentEquipment[slotIdx] = newItem;

    }

    public Equipment Unequip(int slotIdx)
    {
        Equipment oldItem = currentEquipment[slotIdx];
        SkinnedMeshRenderer oldMesh = currentMeshes[slotIdx];


        if (oldItem != null)
        {
            if (oldMesh != null)
            {
                Destroy(oldMesh.gameObject);
            }

            SetEquipmentBlendShapes(oldItem, 0);
            equipmentEventChannel?.RaiseEquipmentChanged(null, oldItem);

            if (!oldItem.isDefaultItem)
            {
                inventorySystemEventChannel?.RaiseLootItemEvent(oldItem);
            }
        }

        currentEquipment[slotIdx] = null;
        return oldItem;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; ++i)
        {
            Unequip(i);
        }

        EquipDefaultItems();
    }

    public void SetEquipmentBlendShapes(Equipment equipment, int weight)
    {
        foreach (EquipmentMeshRegion region in equipment.coveredMeshRegion)
        {
            targetMesh.SetBlendShapeWeight((int)region, weight);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
