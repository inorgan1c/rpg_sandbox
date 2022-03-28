using UnityEngine;

public class CharacterBehaviourFSM : MonoBehaviour
{
    public FSMStateType startState = FSMStateType.Patrol;
    public FSMState[] statePool;
    public FSMState emptyAction;

    FSMState currentState;

    // Start is called before the first frame update
    void Start()
    {
        foreach (FSMState state in statePool)
        {
            state.Init(this);
        }
        currentState = emptyAction;
        TransitionToState(startState);
    }

    private void Update()
    {
        currentState.DoAction();
        FSMStateType nextState = currentState.ShouldTransitionToState();

        if (nextState != currentState.StateName)
        {
            TransitionToState(nextState);
        }
    }

    private void TransitionToState(FSMStateType stateName)
    {
        currentState.OnExit();
        currentState = GetState(stateName);
        currentState.OnEnter();
    }

    FSMState GetState(FSMStateType stateName)
    {
        foreach (var state in statePool)
        {
            if (state.StateName == stateName)
            {
                return state;
            }
        }
        return emptyAction;
    }
}
