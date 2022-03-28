using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Chase State", menuName = "AI/FSM State/Chase State")]

public class ChaseState : FSMState
{
    public float fieldOfView = 60f;
    public override FSMStateType StateName { get { return FSMStateType.Chase; } }

    private NavMeshAgent agent;
    private Perception perception;
    private float startFOV = 0.0f;

    public override void Init(CharacterBehaviourFSM fsm)
    {
        base.Init(fsm);
        agent = Controller.GetComponent<NavMeshAgent>();
        perception = Controller.GetComponent<Perception>();
    }

    public override void DoAction()
    {
        perception.FaceTarget(perception.lastSighting);
    }

    public override void OnEnter()
    {
        startFOV = perception.FieldOfView;
        agent.isStopped = false;

    }

    public override void OnExit()
    {
        perception.FieldOfView = startFOV;
        agent.isStopped = true;
    }

    public override FSMStateType ShouldTransitionToState()
    {
        agent.SetDestination(perception.lastSighting);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return FSMStateType.Attack;

        }
        else if (!perception.isTargetInSightLine)
        {
            return FSMStateType.Patrol;

        }
        else
        {
            return StateName;
        }

    }
}
