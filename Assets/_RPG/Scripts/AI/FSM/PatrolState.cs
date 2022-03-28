using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Patrol State", menuName = "AI/FSM State/Patrol State")]

public class PatrolState : FSMState
{
    public override FSMStateType StateName { get { return FSMStateType.Patrol; } }

    [SerializeField] Vector3[] patrolPath;
    private NavMeshAgent agent;
    private Perception perception;
    private int currentCheckpoint = 0;


    public override void Init(CharacterBehaviourFSM fsm)
    {
        base.Init(fsm);
        agent = Controller.GetComponent<NavMeshAgent>();
        perception = Controller.GetComponent<Perception>();


        if (patrolPath.Length <= 0)
        {
            Debug.Log("Patrol path has no checkpoints!");

        }
        else
        {
            currentCheckpoint = 0;
        }
    }

    public override void DoAction()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentCheckpoint = (currentCheckpoint + 1) % patrolPath.Length;
            agent.SetDestination(patrolPath[currentCheckpoint]);
        }
    }

    public override void OnEnter()
    {
        agent.isStopped = false;
        agent.SetDestination(patrolPath[currentCheckpoint]);
    }

    public override void OnExit()
    {
        agent.isStopped = true;
    }

    public override FSMStateType ShouldTransitionToState()
    {
        if (perception.isTargetInSightLine)
        {
            return FSMStateType.Chase;
        }
        return StateName;
    }
}
