using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public delegate void OnEnemyDeath(CharacterConfig c);
    public static event OnEnemyDeath onEnemyDeath;

    [SerializeField] CharacterConfig config;

    public override void Die()
    {
        if (onEnemyDeath != null)
        {
            onEnemyDeath.Invoke(config);            
        }
        base.Die();
        Destroy(gameObject);
    }
}
