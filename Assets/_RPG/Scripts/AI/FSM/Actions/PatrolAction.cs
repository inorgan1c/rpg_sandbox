using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Patrol Action", menuName = "AI/FSM Action/Patrol Action")]
public class PatrolAction : FSMAction
{
    public override void DoAction(CharacterBehaviourFSM controller)
    {
        Patrol(controller);
    }

    private void Patrol(CharacterBehaviourFSM controller)
    {
        if (controller.currentDest == null || controller.agent.remainingDistance <= controller.agent.stoppingDistance)
        {
            controller.agent.SetDestination(controller.startPos + GetRandomDestination(controller));
        }
    }

    private Vector3 GetRandomDestination(CharacterBehaviourFSM controller)
    {
        Vector2 randDir = Random.insideUnitCircle * controller.patrolAreaRadius;
        return new Vector3(randDir.x, 0, randDir.y);
    }
}
