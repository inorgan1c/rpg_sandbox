using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Chase State", menuName = "AI/FSM State/Chase State")]
public class ChaseState : FSMState
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
