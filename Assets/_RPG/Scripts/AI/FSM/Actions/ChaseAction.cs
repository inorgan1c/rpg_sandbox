using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chase Action", menuName = "AI/FSM Action/Chase Action")]
public class ChaseAction : FSMAction
{
    public override void DoAction(CharacterBehaviourFSM controller)
    {
        Chase(controller);
    }
    
    private void Chase(CharacterBehaviourFSM controller)
    {
        controller.agent.transform.rotation = Perception.FaceTarget(controller.agent.transform, controller.perception.lastSighting);
    }

}
