using System.Collections.Generic;
using UnityEngine;

public class SpellUI : MonoBehaviour
{
    public List<SpellSlotUI> slots;

    [SerializeField] SpellSlotUI slotUIPrefab;
    [SerializeField] SpellEventChannel spellEventChannel;

    private void Awake()
    {
        if (spellEventChannel)
        {
            spellEventChannel.OnSpellLearnt += ShowSpellSlot;
            spellEventChannel.OnInitSpells += Init;
        }
    }

    private void OnDestroy()
    {
        if (spellEventChannel)
        {
            spellEventChannel.OnSpellLearnt -= ShowSpellSlot;
            spellEventChannel.OnInitSpells -= Init;
        }
    }

    void Init(int capacity)
    {
        slots = new List<SpellSlotUI>(capacity);
        for (int i=0; i<capacity; ++i)
        {
            SpellSlotUI slotUI = Instantiate<SpellSlotUI>(slotUIPrefab, transform);
            slots.Add(slotUI);
            slotUI.HideSlot();
        }

        ToggleUI();
    }


    void ShowSpellSlot(Spell spell)
    {
        SpellSlotUI slot = slots.Find(s => s.spell == null);
        if (slot != null)
        {
            slot.spell = spell;
            slot.ShowSlot();
        }
    }

    public void OnClearSlot(SpellSlotUI slot)
    {
        spellEventChannel.RaiseSpellUnlearnt(slot.spell);
    }

    public void OnEquipSpell(Spell spell)
    {
        spellEventChannel.RaiseEquipSpell(spell);
    }

    public void ToggleUI()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}

    
