using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Decision", menuName = "AI/FSM Decision/Attack Decision")]
public class AttackDecision : FSMDecision
{
    public override bool MakeDecision(CharacterBehaviourFSM controller)
    {
        return InAttackRange(controller);
    }

    private bool InAttackRange(CharacterBehaviourFSM controller)
    {
        if (controller.perception)
        {
            return (Vector3.Distance(controller.transform.position, controller.target.position) <= controller.perception.proximityRadius);
        } else
        {
            return false;
        }

    }



}
