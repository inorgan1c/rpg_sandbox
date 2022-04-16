using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Wander State", menuName = "AI/FSM State/Wander State")]
public class WanderState : FSMState
{
    public override void OnEnter(CharacterBehaviourFSM controller)
    {
        controller.agent.isStopped = false;
    }

    public override void OnExit(CharacterBehaviourFSM controller)
    {
        controller.agent.isStopped = true;
    }

}
