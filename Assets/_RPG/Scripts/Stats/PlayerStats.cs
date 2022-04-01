using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats  
{
    int xp = 0;

    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }

    public void UpdateXP(int newXP)
    {
        xp += newXP;
        Debug.Log("Player XP: " + xp + "(+" + newXP + ")");
    } 


    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}
