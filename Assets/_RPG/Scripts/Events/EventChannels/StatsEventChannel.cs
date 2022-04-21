using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Stats Event Channel", menuName = "Event/Stats Event Channel")]
public class StatsEventChannel : ScriptableObject
{
    public UnityAction<int, int, int> OnHealthChanged;
    public UnityAction<int, int, int> OnManaChanged;
    public UnityAction<int, int, int> OnEnergyChanged;
    public UnityAction<int, int> OnIntelligenceChanged;
    public UnityAction<int, int> OnArmorChanged;
    public UnityAction<int, int> OnDamageChanged;
    public UnityAction<int, int> OnXPChanged;
    public UnityAction<CharacterConfig> OnEnemyDeath;
    public UnityAction OnSleep;
    public UnityAction OnWakeUp;

    public void RaiseHealthChanged(int id, int maxHealth, int currentHealth)
    {
        OnHealthChanged?.Invoke(id, maxHealth, currentHealth);
    }

    public void RaiseManaChanged(int id, int maxMana, int currentMana)
    {
        OnManaChanged?.Invoke(id, maxMana, currentMana);
    }

    public void RaiseEnergyChanged(int id, int maxEnergy, int currentEnergy)
    {
        OnEnergyChanged?.Invoke(id, maxEnergy, currentEnergy);
    }

    public void RaiseIntelligenceChanged(int id, int currentIntelligence)
    {
        OnIntelligenceChanged?.Invoke(id, currentIntelligence);
    }

    public void RaiseArmorChanged(int id, int currentArmor)
    {
        OnArmorChanged?.Invoke(id, currentArmor);
    }

    public void RaiseDamageChanged(int id, int currentDamage)
    {
        OnDamageChanged?.Invoke(id, currentDamage);
    }

    public void RaiseXPChanged(int id, int currentXP)
    {
        OnDamageChanged?.Invoke(id, currentXP);
    }

    public void RaiseEnemyDeath(CharacterConfig config)
    {
        OnEnemyDeath?.Invoke(config);
    }

    public void RaiseSleep()
    {
        OnSleep?.Invoke();
    }

    public void RaiseWakeUp()
    {
        OnWakeUp?.Invoke();
    }

}
