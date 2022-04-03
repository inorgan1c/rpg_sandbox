using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Escape State", menuName = "AI/FSM State/Escape State")]
public class EscapeState : FSMState
{
    public string targetTag = "Player";

    public float runMultiplier = 3f;

    private NavMeshAgent agent;
    private Transform target;
    private CharacterStats stats;
    private Perception perception;

    public override FSMStateType StateName { get { return FSMStateType.Escape; } }

    public override void Init(CharacterBehaviourFSM fsm)
    {
        base.Init(fsm);
        agent = Controller.GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player;       
        perception = Controller.GetComponent<Perception>();
    }


    public override void OnEnter() {
        perception.EscapeTarget(target.position);
        agent.isStopped = false;
        agent.speed = agent.speed * runMultiplier;
    }
    public override void OnExit() {

        agent.speed = agent.speed / runMultiplier;
        agent.isStopped = true;
    }
    public override void DoAction() {
        perception.EscapeTarget(target.position);
    }

    public override FSMStateType ShouldTransitionToState() {
        
        agent.SetDestination(agent.transform.forward*perception.sightRadius);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return FSMStateType.Wander;

        }
        else
        {
            return StateName;
        }

    }

}
