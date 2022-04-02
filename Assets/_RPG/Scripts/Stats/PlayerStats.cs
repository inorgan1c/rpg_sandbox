using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats  
{
    int xp = 0;
    public static float awakenTime;

    public TimedStat energy;

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;    
        TimeManager.onNewHour += OnNewHour;
        TimeManager.onNewDay += OnNewDay; //temporary!!! fixed when sleep time will be implemented

        energy.Init();
        OnNewDay();
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

    public void OnNewHour()
    {
        awakenTime += 1/24f;

    }

    public void OnNewDay()
    {
        awakenTime = 0;
    }
}
