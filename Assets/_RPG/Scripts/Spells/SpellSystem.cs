using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSystem : MonoBehaviour
{
    [SerializeField] List<Spell> spells;
    [SerializeField] float spellCoolDown = 3f;

    private float currentCoolDown = 0f;
    private GameObject gfx;
    Spell equippedSpell = null;
    
    private void Update()
    {
        currentCoolDown -= Time.deltaTime;

        #region equiptest
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(spells[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Equip(spells[1]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Equip(spells[2]);
        }
        #endregion
    }

    public void Equip(Spell newSpell)
    {

        Unequip();

        equippedSpell = newSpell;
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
