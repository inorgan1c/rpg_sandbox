using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Escape Decision", menuName = "AI/FSM Decision/Escape Decision")]
public class EscapeDecision : FSMDecision
{
    public override bool MakeDecision(CharacterBehaviourFSM controller)
    {
        return Escaped(controller);
    }

    private bool Escaped(CharacterBehaviourFSM controller)
    {
        if (controller.perception)
        {
            float distance = Vector3.Distance(controller.perception.lastSighting, controller.transform.position);
            return (distance > controller.perception.sightRadius);
        }
        else
        {
            return false;
        }
    }
}
