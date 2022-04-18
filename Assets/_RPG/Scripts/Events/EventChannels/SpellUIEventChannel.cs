using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New SpellUI Event Channel", menuName = "Event/SpellUI Event Channel")]
public class SpellUIEventChannel : ScriptableObject
{
    public UnityAction<Spell> OnSpellLearnt;
    public UnityAction<Spell> OnSpellUnLearnt;

    public void RaiseSpellLearnt(Spell spell)
    {
        OnSpellLearnt?.Invoke(spell);
    }

    public void RaiseSpellUnlearnt(Spell spell)
    {
        OnSpellUnLearnt?.Invoke(spell);
    }
}
