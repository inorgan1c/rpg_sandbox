using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kill Goal", menuName = "Quests/Goals/Kill Goal")]
public class KillGoal : Quest.QuestGoal
{
    public CharacterConfig.CharacterClassType targetClass;
    
    public override string Description()
    {
        string descr = "Kill " + requiredAmount + " " + targetClass;
        return descr;
    }

    public override void Initialize()
    {
        base.Initialize();
        CurrentAmount = 0;
        EnemyStats.onEnemyDeath += OnKill;
    }

    private void OnKill(CharacterConfig config)
    {
        if (config.charClass == targetClass)
        {
            CurrentAmount++;
            Evaluate();
        }
    }
}
