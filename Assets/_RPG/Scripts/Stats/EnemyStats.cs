using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] CharacterConfig config;

    public override void Die()
    {
        statsEventChannel.RaiseEnemyDeath(config);

        base.Die();
        Destroy(gameObject);
    }
}
