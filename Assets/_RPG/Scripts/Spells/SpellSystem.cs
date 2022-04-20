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
    [SerializeField] SpellEventChannel spellEventChannel;
    [SerializeField] InventorySystemEventChannel inventorySystemEventChannel;


    private float currentCoolDown = 0f;
    private GameObject gfx;
    Spell equippedSpell = null;
    PlayerStats stats;


    private void OnEnable()
    {
        spellEventChannel.OnEquipSpell += Equip;
        spellEventChannel.OnSpellUnLearnt += Unlearn;
    }

    private void OnDisable()
    {
        if (spellEventChannel)
        {
            spellEventChannel.OnEquipSpell -= Equip;
        }
    }


    private void Start()
    {
        stats = GetComponent<PlayerStats>();

        spellSlots = new List<SpellSlot>(stats.intelligence.GetValue());
        for (int i = 0; i < stats.intelligence.GetValue(); ++i)
        {
            spellSlots.Add(new SpellSlot());
        }

        spellEventChannel.RaiseInitSpells(stats.intelligence.GetValue());
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

            spellEventChannel.RaiseSpellLearnt(newSpell);
        }
    }

    public void Unlearn(Spell spell)
    {
        SpellSlot slot = spellSlots.Find(slot => slot.spell == spell);
        if (slot != null)
        {
            if (slot.spell == equippedSpell)
            {
                Unequip();
            }

            inventorySystemEventChannel.OnLootItem(slot.spell, 1);
            slot.spell = null;
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
            equippedSpell = null;
        }
    }


    public void Cast(Enemy enemy)
    {
        if (equippedSpell && currentCoolDown <= 0 && (stats.mana.GetValue() >= equippedSpell.mana))
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= equippedSpell.range)
            {
                gfx.transform.position = enemy.transform.position + equippedSpell.spawnOffset;
                gfx.GetComponentInChildren<ParticleSystem>().Play();
                enemy.GetComponent<EnemyStats>()?.TakeDamage(equippedSpell.damage);

                spellEventChannel.OnCastSpell(equippedSpell);
                currentCoolDown = spellCoolDown;
            }
        }   
    }
}
