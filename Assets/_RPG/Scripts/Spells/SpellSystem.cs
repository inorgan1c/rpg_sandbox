using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellSlot
{
    public Spell spell = null;
}

public class SpellSystem : MonoBehaviour
{
    [SerializeField] List<SpellSlot> spellSlots;
    [SerializeField] float spellCoolDown = 3f;
    [SerializeField] int capacity = 3;
    [SerializeField] SpellUIEventChannel spellUIEventChannel;


    private float currentCoolDown = 0f;
    private GameObject gfx;
    Spell equippedSpell = null;

    private void Start()
    {
        spellSlots = new List<SpellSlot>(capacity);
        for (int i = 0; i<capacity; ++i)
        {
            spellSlots.Add(new SpellSlot());
        }
    }

    private void Update()
    {
        currentCoolDown -= Time.deltaTime;
    }

    public void Learn(Spell newSpell)
    {
        SpellSlot slot = spellSlots.Find(slot => slot.spell == null);
        if (slot != null)
        {
            slot.spell = newSpell;

            spellUIEventChannel.RaiseSpellLearnt(newSpell);
        }
    }

    public void Equip(Spell spell)
    {

        Unequip();

        equippedSpell = spell;
        gfx = Instantiate(equippedSpell.gfx, transform.position, Quaternion.identity, transform);
    }

    public void Unequip()
    {
        if (gfx)
        {
            Destroy(gfx);
            gfx = null;
        }
    }


    public void Cast(Enemy enemy)
    {
        if (equippedSpell && currentCoolDown <= 0)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= equippedSpell.range)
            {
                gfx.transform.position = enemy.transform.position + equippedSpell.spawnOffset;
                gfx.GetComponentInChildren<ParticleSystem>().Play();

                enemy.GetComponent<EnemyStats>()?.TakeDamage(equippedSpell.damage);
                
                currentCoolDown = spellCoolDown;
            }
        }   
    }
}
