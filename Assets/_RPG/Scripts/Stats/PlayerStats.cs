using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats  
{
    int xp = 0;
    public static float awakenTime;
    public Stat intelligence;
    public EnergyStat energy;
    public ManaStat mana;

    [SerializeField] TimeEventChannel timeEventChannel;
    [SerializeField] QuestEventChannel questEventChannel;
    [SerializeField] EquipmentEventChannel equipmentEventChannel;
    [SerializeField] SpellEventChannel spellEventChannel;

    private void OnEnable()
    {
        equipmentEventChannel.OnEquipmentChanged += OnEquipmentChanged;
        timeEventChannel.OnNewHour += OnNewHour;
        timeEventChannel.OnNewDay += OnNewDay;
        spellEventChannel.OnCastSpell += UpdateMana;
        questEventChannel.OnQuestCompleted += UpdateXP;
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
            timeEventChannel.OnNewDay += OnNewDay; 
        }

    }

    void Start()
    {
        energy.Init(gameObject.GetInstanceID(), timeEventChannel, statsEventChannel);
        mana.Init(gameObject.GetInstanceID(), timeEventChannel, statsEventChannel);
        OnNewDay();

        statsEventChannel.OnArmorChanged(gameObject.GetInstanceID(), armor.GetValue());
        statsEventChannel.OnDamageChanged(gameObject.GetInstanceID(), damage.GetValue());
        statsEventChannel.OnIntelligenceChanged(gameObject.GetInstanceID(), intelligence.GetValue());
        statsEventChannel.OnEnergyChanged(gameObject.GetInstanceID(), energy.GetBaseValue(), energy.GetValue());
        statsEventChannel.OnManaChanged(gameObject.GetInstanceID(), mana.GetBaseValue(), mana.GetValue());
        statsEventChannel.OnHealthChanged(gameObject.GetInstanceID(), maxHealth, currentHealth);
        statsEventChannel.OnXPChanged(gameObject.GetInstanceID(), xp);
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

        statsEventChannel.OnArmorChanged(gameObject.GetInstanceID(), armor.GetValue());
        statsEventChannel.OnDamageChanged(gameObject.GetInstanceID(), damage.GetValue());
    }

    public void UpdateXP(Quest quest)
    {
        UpdateXP(quest.reward.xp);
    }

    public void UpdateXP(int newXP)
    {
        xp += newXP;
        statsEventChannel.OnXPChanged(gameObject.GetInstanceID(), xp);

        Debug.Log("Player XP: " + xp + "(+" + newXP + ")");
    } 

    public void RestoreEnergy(int ep)
    {
        energy.Restore(ep);
        statsEventChannel.OnEnergyChanged(gameObject.GetInstanceID(), energy.GetBaseValue(), energy.GetValue());
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

    public void UpdateMana(Spell spell)
    {
        mana.Use(spell.mana);
        statsEventChannel.OnManaChanged(gameObject.GetInstanceID(), mana.GetBaseValue(), mana.GetValue());
    }

    public void RestoreMana(int mp)
    {
        mana.Restore(mp);
        statsEventChannel.OnManaChanged(gameObject.GetInstanceID(), mana.GetBaseValue(), mana.GetValue());
    }

    public void Sleep()
    {
        statsEventChannel.RaiseSleep();
        Time.timeScale = 4f;
    }

    public void WakeUp()
    {
        statsEventChannel.RaiseWakeUp();
        Time.timeScale = 1f;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            Sleep();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            WakeUp();
        }
    }
}

