using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Action", menuName = "AI/FSM Action/Attack Action")]
public class AttackAction : FSMAction
{
    public override void DoAction(CharacterBehaviourFSM controller)
    {
        Attack(controller);
    }

    private void Attack(CharacterBehaviourFSM controller)
    {
        controller.isAttacking = Vector3.Distance(controller.transform.position, controller.target.position) < controller.agent.stoppingDistance;
        if (!controller.isAttacking)
        {
            controller.agent.isStopped = false;
            controller.agent.SetDestination(controller.target.position);
        }
        else
        {
            controller.agent.transform.rotation = Perception.FaceTarget(controller.agent.transform, controller.target.position);
            controller.combat.Attack(controller.targetStats);
        }
    }
}
