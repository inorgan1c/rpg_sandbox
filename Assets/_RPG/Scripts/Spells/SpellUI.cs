using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUI : MonoBehaviour
{
    public List<SpellSlotUI> slots;

    [SerializeField] SpellUIEventChannel spellUIEventChannel;

    private void Awake()
    {
        if (spellUIEventChannel)
        {
            spellUIEventChannel.OnSpellLearnt += ShowSpellSlot;
        }
    }

    private void OnDestroy()
    {
        if (spellUIEventChannel)
        {
            spellUIEventChannel.OnSpellLearnt -= ShowSpellSlot;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        slots = new List<SpellSlotUI>(GetComponentsInChildren<SpellSlotUI>());
        slots.ForEach(slot => slot.HideSlot());
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

    public void ToggleUI()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
