using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FSMStateType
{
    None, 
    Patrol,
    Chase,
    Attack,
    Escape,
    Wander
}

public class FSMState : ScriptableObject
{
    CharacterBehaviourFSM controller = null;
    public CharacterBehaviourFSM Controller
    {
        get
        {
            return controller;
        }
        set
        {
            controller = value;
        }
    }

    public virtual FSMStateType StateName { get;  }

    public virtual void Init(CharacterBehaviourFSM fsm) {
        Controller = fsm;
    }
    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void DoAction() { }

    public virtual FSMStateType ShouldTransitionToState() { return StateName; }
}