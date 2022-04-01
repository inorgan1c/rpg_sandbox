using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Patrol State", menuName = "AI/FSM State/Patrol State")]
public class PatrolState : FSMState
{
    public override FSMStateType StateName { get { return FSMStateType.Patrol; } }

    [SerializeField] float patrolAreaRadius;
    private NavMeshAgent agent;
    private Perception perception;
    private Vector3 startPos;
    private Vector3 currentDest;

    public override void Init(CharacterBehaviourFSM fsm)
    {
        base.Init(fsm);
        agent = Controller.GetComponent<NavMeshAgent>();
        perception = Controller.GetComponent<Perception>();
        startPos = agent.transform.position;

    }

    public override void DoAction()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(startPos + GetRandomDestination());
        }
    }

    public override void OnEnter()
    {
        agent.isStopped = false;
        currentDest = startPos + GetRandomDestination();
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

    private Vector3 GetRandomDestination()
    {
        Vector2 randDir = Random.insideUnitCircle * patrolAreaRadius;
        return new Vector3(randDir.x, 0, randDir.y);
    }
}
