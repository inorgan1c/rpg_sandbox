using UnityEngine;
using UnityEngine.AI;

public class CharacterBehaviourFSM : MonoBehaviour
{
    public FSMState startState;
    public FSMState emptyState;

    FSMState currentState;

    public Vector3 startPos;
    public  Vector3 currentDest;
    public NavMeshAgent agent;
    public Perception perception;
    public string targetTag = "Player";
    public Transform target;
    public CharacterStats stats;
    public bool isAttacking = false;
    public CharacterCombat combat;
    public CharacterStats targetStats;
    public float currentWait;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        perception = GetComponent<Perception>();
        target = PlayerManager.instance.player;
        targetStats = target.GetComponent<CharacterStats>();
        stats = GetComponent<CharacterStats>();
        combat = GetComponent<CharacterCombat>();
        currentState = emptyState;
        TransitionToState(startState);
    }

    private void Update()
    {
        currentState.DoAction(this);

        currentState.CheckTransitions(this);
    }

    public void TransitionToState(FSMState state)
    {
        currentState.OnExit(this);
        currentState = state;
        currentState.OnEnter(this);
    }
}
