using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Escape State", menuName = "AI/FSM State/Escape State")]
public class EscapeState : FSMState
{
    public override void OnEnter(CharacterBehaviourFSM controller)
    {
        controller.agent.transform.rotation = Perception.EscapeTarget(controller.agent.transform, controller.target.position);
        controller.agent.isStopped = false;
        controller.agent.speed = controller.agent.speed * controller.stats.Config.runMultiplier;
    }
    public override void OnExit(CharacterBehaviourFSM controller)
    {

        controller.agent.speed = controller.agent.speed / controller.stats.Config.runMultiplier;
        controller.agent.isStopped = true;
    }
}
