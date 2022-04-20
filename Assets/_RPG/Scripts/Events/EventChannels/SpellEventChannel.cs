using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Spell Event Channel", menuName = "Event/Spell Event Channel")]
public class SpellEventChannel : ScriptableObject
{
    public UnityAction<int> OnInitSpells;
    public UnityAction<Spell> OnCastSpell;
    public UnityAction<Spell> OnSpellLearnt;
    public UnityAction<Spell> OnSpellUnLearnt;
    public UnityAction<Spell> OnEquipSpell;
    public UnityAction<Spell> OnUnequipSpell;

    public void RaiseInitSpells(int capacity)
    {
        OnInitSpells?.Invoke(capacity);
    }

    public void RaiseCastSpell(Spell spell)
    {
        OnCastSpell?.Invoke(spell);
    }

    public void RaiseSpellLearnt(Spell spell)
    {
        OnSpellLearnt?.Invoke(spell);
    }

    public void RaiseSpellUnlearnt(Spell spell)
    {
        OnSpellUnLearnt?.Invoke(spell);
    }

    public void RaiseEquipSpell(Spell spell)
    {
        OnEquipSpell?.Invoke(spell);
    }

    public void RaiseUnequipSpell(Spell spell)
    {
        OnUnequipSpell?.Invoke(spell);
    }
}
