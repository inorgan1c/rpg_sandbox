using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(fileName = "New Attack State", menuName = "AI/FSM State/Attack State")]
public class AttackState : FSMState
{
    public override void OnEnter(CharacterBehaviourFSM controller)
    {
        controller.combat.Attack(controller.targetStats);
    }

    public override void OnExit(CharacterBehaviourFSM controller)
    {
        controller.agent.isStopped = true;
        controller.isAttacking = false;
    }

}
