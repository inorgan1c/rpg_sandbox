using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Patrol State", menuName = "AI/FSM State/Patrol State")]
public class PatrolState : FSMState
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

