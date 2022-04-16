using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wander Action", menuName = "AI/FSM Action/Wander Action")]
public class WanderAction : FSMAction
{
    public override void DoAction(CharacterBehaviourFSM controller)
    {
        Wander(controller);
    }

    private void Wander(CharacterBehaviourFSM controller)
    {
        if (controller.currentDest == null || controller.agent.remainingDistance <= controller.agent.stoppingDistance)
        {
            if (controller.currentWait <= 0)
            {
                controller.agent.SetDestination(controller.startPos + GetRandomDestination(controller));
                controller.currentWait = Random.Range(0.0f, controller.stats.Config.maxWaitPeriod);
            }
            else
            {
                controller.currentWait -= Time.deltaTime;
            }
        }
    }

    private Vector3 GetRandomDestination(CharacterBehaviourFSM controller)
    {
        Vector2 randDir = Random.insideUnitCircle * controller.stats.Config.patrolAreaRadius;
        return new Vector3(randDir.x, 0, randDir.y);
    }
}
