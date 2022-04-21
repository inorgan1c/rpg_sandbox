using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] Text healthValText;
    [SerializeField] Text manaValText;
    [SerializeField] Text intelligenceValText;
    [SerializeField] Text damageValText;
    [SerializeField] Text armorValText;
    [SerializeField] Text energyValText;
    [SerializeField] Text xpValText;
    [SerializeField] StatsEventChannel statsEventChannel;

    int targetID;

    void Awake()
    {
        statsEventChannel.OnHealthChanged += UpdateHealthUI;
        statsEventChannel.OnManaChanged += UpdateManaUI;
        statsEventChannel.OnIntelligenceChanged += UpdateIntelligenceUI;
        statsEventChannel.OnDamageChanged += UpdateDamageUI;
        statsEventChannel.OnArmorChanged += UpdateArmorUI;
        statsEventChannel.OnEnergyChanged += UpdateEnergyUI;
        statsEventChannel.OnXPChanged += UpdateXPUI;
    }

    private void OnDestroy()
    {
        statsEventChannel.OnHealthChanged -= UpdateHealthUI;
        statsEventChannel.OnManaChanged -= UpdateManaUI;
        statsEventChannel.OnIntelligenceChanged -= UpdateIntelligenceUI;
        statsEventChannel.OnDamageChanged -= UpdateDamageUI;
        statsEventChannel.OnArmorChanged -= UpdateArmorUI;
        statsEventChannel.OnEnergyChanged -= UpdateEnergyUI;
        statsEventChannel.OnXPChanged -= UpdateXPUI;
    }

    private void Start()
    {
        targetID = PlayerManager.instance.player.gameObject.GetInstanceID();
    }

    void UpdateHealthUI(int id, int baseValue, int currentValue)
    {
        if (targetID == id)
        {
            healthValText.text = currentValue + "/" + baseValue;
        }
    }

    void UpdateManaUI(int id, int baseValue, int currentValue)
    {
        if (targetID == id)
        {
            manaValText.text = currentValue + "/" + baseValue;
        }
    }

    void UpdateIntelligenceUI(int id, int currentValue)
    {
        if (targetID == id)
        {
            intelligenceValText.text = currentValue.ToString();
        }
    }

    void UpdateEnergyUI(int id, int baseValue, int currentValue)
    {
        if (targetID == id)
        {
            energyValText.text = currentValue + "/" + baseValue;
        }
    }

    void UpdateDamageUI(int id, int value)
    {
        if (targetID == id)
        {
            damageValText.text = value.ToString();
        }
    }

    void UpdateArmorUI(int id, int value)
    {
        if (targetID == id)
        {
            armorValText.text = value.ToString();
        }
    }

    void UpdateXPUI(int id, int value)
    {
        if (targetID == id)
        {
            xpValText.text = value.ToString();
        }
    }
}
