using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    const float locomotionAnimationSmoothingTime = 0.1f;

    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    public AnimatorOverrideController overrideController;

    NavMeshAgent agent;
    protected Animator animator;
    protected CharacterCombat combat;
    protected AnimationClip[] currentAttackAnimSet;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        if (!overrideController)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("SpeedPercent", speedPercent, locomotionAnimationSmoothingTime, Time.deltaTime);
        animator.SetBool("InCombat", combat.InCombat);
    }

    public virtual void OnAttack()
    {
        animator.SetTrigger("Attack");

        int animIdx = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[animIdx];
    }
}
