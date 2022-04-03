using UnityEngine;

public class CharacterBehaviourFSM : MonoBehaviour
{
    public FSMStateType startState = FSMStateType.Patrol;
    public FSMState[] statePool;
    public FSMState emptyAction;

    FSMState[] statePoolInstances;
    FSMState currentState;

    // Start is called before the first frame update
    void Start()
    {
        statePoolInstances = new FSMState[statePool.Length];
        for (int i = 0; i < statePool.Length; ++i)
        {
            statePoolInstances[i] = FSMState.Instantiate(statePool[i]);
            statePoolInstances[i].Init(this);
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
        Debug.Log("Transition to: " + stateName);
        currentState.OnExit();
        currentState = GetState(stateName);
        currentState.OnEnter();
    }

    FSMState GetState(FSMStateType stateName)
    {
        foreach (var state in statePoolInstances)
        {
            if (state.StateName == stateName)
            {
                return state;
            }
        }
        return emptyAction;
    }
}
