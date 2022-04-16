using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Perception Decision", menuName = "AI/FSM Decision/Perception Decision")]
public class PerceptionDecision : FSMDecision
{
    public override bool MakeDecision(CharacterBehaviourFSM controller)
    {
        return Look(controller);
    }

    private bool Look(CharacterBehaviourFSM controller)
    {
        if (controller.perception)
        {
            return controller.perception.isTargetInSightLine;
        } else
        {
            return false;
        }
    }
}
