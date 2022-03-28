using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(fileName = "New Attack State", menuName = "AI/FSM State/Attack State")]
public class AttackState : FSMState
{
    public string targetTag = "Player";

    private NavMeshAgent agent;
    private bool isAttacking = false;
    private Transform target;
    private Perception perception;
    CharacterCombat combat;
    CharacterStats targetStats;

    public override FSMStateType StateName { get { return FSMStateType.Attack; } }

    public override void Init(CharacterBehaviourFSM fsm)
    {
        base.Init(fsm);
        agent = Controller.GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player;
        targetStats = target.GetComponent<CharacterStats>();
        combat = Controller.GetComponent<CharacterCombat>();
        perception = Controller.GetComponent<Perception>();
    }

    public override void DoAction()
    {
        isAttacking = Vector3.Distance(Controller.transform.position, target.position) < agent.stoppingDistance;
        if (!isAttacking)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        } else
        {
            perception.FaceTarget(target.position);
            combat.Attack(targetStats);
        }
    }

    public override void OnEnter()
    {
        combat.Attack(targetStats);
    }

    public override void OnExit()
    {
        agent.isStopped = true;
        isAttacking = false;
    }

    public override FSMStateType ShouldTransitionToState()
    {
        bool Escaped = Vector3.Distance(Controller.transform.position, target.position) > perception.proximityRadius;
        if (Escaped)
        {
            return FSMStateType.Chase;
        }

        return StateName;
    }
}
