using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Escape Action", menuName = "AI/FSM Action/Escape Action")]
public class EscapeAction : FSMAction
{
    public override void DoAction(CharacterBehaviourFSM controller)
    {
        Escape(controller);
    }

    private void Escape(CharacterBehaviourFSM controller)
    {
        if (controller.agent.remainingDistance <= controller.agent.stoppingDistance)
        {
            controller.agent.transform.rotation = Perception.EscapeTarget(controller.agent.transform, controller.target.position);
            controller.currentDest = controller.agent.transform.forward * controller.perception.sightRadius;
            controller.agent.SetDestination(controller.currentDest);

        }
    }
}
