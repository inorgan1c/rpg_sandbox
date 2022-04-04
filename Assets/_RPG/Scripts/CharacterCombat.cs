using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackDelay = 0.6f;
    public float attackSpeed = 1f;
    public event System.Action OnAttack;

    public bool InCombat { get; private set; }

    float attackCoolDown = 0f;
    float combatCoolDown = 5f;
    float lastAttackTime;
    CharacterStats myStats;
    CharacterStats opponentStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCoolDown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCoolDown)
        {
            InCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCoolDown <= 0)
        {
            Debug.Log(targetStats.gameObject.name);
            opponentStats = targetStats;
            if (OnAttack != null)
            {
                OnAttack.Invoke();
            }
            attackCoolDown = 1f / attackSpeed;

            InCombat = true;
            lastAttackTime = Time.time;
        }
    }

    public void AttackHit_AnimationEvent()
    {
        opponentStats.TakeDamage(myStats.damage.GetValue());

        if (opponentStats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }

}
