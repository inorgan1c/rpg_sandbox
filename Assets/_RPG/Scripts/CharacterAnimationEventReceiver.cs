using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEventReceiver : MonoBehaviour
{
    public CharacterCombat combat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AttackHitEvent()
    {
        combat.AttackHit_AnimationEvent();
    }
}
