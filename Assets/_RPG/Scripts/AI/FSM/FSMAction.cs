using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMAction : ScriptableObject
{
    public abstract void DoAction(CharacterBehaviourFSM controller);
}
