using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats  
{
    int xp = 0;
    public static float awakenTime;
    public TimedStat energy;

    [SerializeField] TimeEventChannel timeEventChannel;
    [SerializeField] EquipmentEventChannel equipmentEventChannel;

    private void OnEnable()
    {
        equipmentEventChannel.OnEquipmentChanged += OnEquipmentChanged;
        timeEventChannel.OnNewHour += OnNewHour;
        timeEventChannel.OnNewDay += OnNewDay; //temporary!!! fixed when sleep time will be implemented
    }

    private void OnDisable()
    {
        if (equipmentEventChannel)
        {
            equipmentEventChannel.OnEquipmentChanged += OnEquipmentChanged;
        }

        if (timeEventChannel)
        {
            timeEventChannel.OnNewHour += OnNewHour;
            timeEventChannel.OnNewDay += OnNewDay; //temporary!!! fixed when sleep time will be implemented
        }

    }

    void Start()
    {
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

    public void RestoreEnergy(int ep)
    {
        energy.Restore(ep);

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
