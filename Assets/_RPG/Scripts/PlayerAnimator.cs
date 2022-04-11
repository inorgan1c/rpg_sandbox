using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict;

    [SerializeField] InventoryEventChannel inventoryEventChannel;

    private void OnEnable()
    {
        inventoryEventChannel.OnEquipmentChanged += OnEquipmentChange;
    }

    private void OnDisable()
    {
        if (inventoryEventChannel)
        {
            inventoryEventChannel.OnEquipmentChanged -= OnEquipmentChange;
        }
    }

    protected override void Start()
    {
        base.Start();

        weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]>();
        foreach (WeaponAnimations a in weaponAnimations)
        {
            weaponAnimationsDict.Add(a.weapon, a.clips);
        }
    }

    void OnEquipmentChange(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 1); //set mask for grip_right anim
            if (weaponAnimationsDict.ContainsKey(newItem))
            {
                currentAttackAnimSet = weaponAnimationsDict[newItem];   //set attack anim set
            }

        } else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 0); //unset mask for grip_right anim
            currentAttackAnimSet = defaultAttackAnimSet; //unse weapon attack anim set
        }

        if (newItem != null && newItem.equipSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 1); //set mask for grip_left anim
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 0); //unset mask for grip_left anim
        }
    }

    [System.Serializable]
    public struct WeaponAnimations 
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}
