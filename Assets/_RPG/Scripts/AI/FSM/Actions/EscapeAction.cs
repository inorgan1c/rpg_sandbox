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
        controller.agent.transform.rotation = Perception.EscapeTarget(controller.agent.transform, controller.target.position);
    }
}
