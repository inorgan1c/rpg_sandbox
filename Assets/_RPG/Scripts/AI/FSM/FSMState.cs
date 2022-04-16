using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class FSMState : ScriptableObject
{
    public FSMAction[] actions;
    public FSMTransition[] transitions;

    public virtual void OnEnter(CharacterBehaviourFSM controller) { }
    public virtual void OnExit(CharacterBehaviourFSM controller) { }
    public virtual void DoAction(CharacterBehaviourFSM controller) { 
        foreach (FSMAction action in actions)
        {
            action.DoAction(controller);
        }
    }

    public virtual void CheckTransitions(CharacterBehaviourFSM controller) { 
        foreach (FSMTransition transition in transitions)
        {
            bool decision = transition.decision.MakeDecision(controller);
            if (decision)
            {
                controller.TransitionToState(transition.trueState);
            } else
            {
                controller.TransitionToState(transition.falseState);
            }
        }
    }
}