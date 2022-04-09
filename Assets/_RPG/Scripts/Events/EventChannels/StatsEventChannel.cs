using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Stats Event Channel", menuName = "Event/Stats Event Channel")]
public class StatsEventChannel : ScriptableObject
{
    public UnityAction<int, int, int> OnHealthChanged;
    public UnityAction<CharacterConfig> OnEnemyDeath;


    public void RaiseHealthChanged(int id, int maxHealth, int currentHealth)
    {
        OnHealthChanged?.Invoke(id, maxHealth, currentHealth);
    }

    public void RaiseEnemyDeath(CharacterConfig config)
    {
        OnEnemyDeath?.Invoke(config);
    }
}
