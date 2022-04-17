using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellClass
{
    Ice,
    Fire,
    Bolt
}

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell System/New Spell")]
public class Spell : ScriptableObject
{
    public new string name;
    public SpellClass spellClass;
    public int damage;
    public int mana;
    public float range;
    public Vector3 spawnOffset;
    public GameObject gfx;
}
