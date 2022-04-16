using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        statsEventChannel.RaiseEnemyDeath(Config);

        base.Die();
        Destroy(gameObject);
    }
}
