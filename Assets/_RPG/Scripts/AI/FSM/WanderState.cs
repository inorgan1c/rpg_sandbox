using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Wander State", menuName = "AI/FSM State/Wander State")]
public class WanderState : FSMState
{
    public override FSMStateType StateName { get { return FSMStateType.Wander; } }

    [SerializeField] float wanderAreaRadius;
    [SerializeField] float maxWaitPeriod;
    private NavMeshAgent agent;
    private Perception perception;
    private Vector3 startPos;
    private Vector3 currentDest;
    private float currentWait;

    public override void Init(CharacterBehaviourFSM fsm)
    {
        base.Init(fsm);
        agent = Controller.GetComponent<NavMeshAgent>();
        perception = Controller.GetComponent<Perception>();
        startPos = agent.transform.position;
        currentWait = maxWaitPeriod;
    }

    public override void DoAction()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (currentWait <= 0)
            {
                agent.SetDestination(startPos + GetRandomDestination());
                currentWait = Random.Range(0.0f, maxWaitPeriod);
            }
            else
            {
                currentWait -= Time.deltaTime;
            }
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
            return FSMStateType.Escape;
        }
        return StateName;
    }

    private Vector3 GetRandomDestination()
    {
        Vector2 randDir = Random.insideUnitCircle * wanderAreaRadius;
        return new Vector3(randDir.x, 0, randDir.y);
    }
}
