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
public class Spell : Item
{
    public SpellClass spellClass;
    public int damage;
    public int mana;
    public float range;
    public Vector3 spawnOffset;
    public GameObject gfx;


    public override void Use()
    {
        SpellSystem spellSystem = PlayerManager.instance.player.GetComponent<SpellSystem>();
        spellSystem.Learn(this);
    }
}
