using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Empty State", menuName = "AI/FSM State/Empty State")]
public class EmptyState : FSMState
{

    public FSMStateType StateName { get { return FSMStateType.None; } }

    public override FSMStateType ShouldTransitionToState()
    {
        return StateName;
    }

}