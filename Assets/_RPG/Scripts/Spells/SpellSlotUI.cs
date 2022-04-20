using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlotUI : MonoBehaviour
{
    public Spell spell;
    public Image icon;
    public Button removeBtn;

    public void ShowSlot()
    {
        if (spell)
        {
            icon.gameObject.SetActive(true);
            icon.sprite = spell.Icon;
            removeBtn.gameObject.SetActive(true);
        }
    }

    public void HideSlot()
    {
        spell = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        removeBtn.gameObject.SetActive(false);
    }

    public void Use()
    {
        if (spell)
        {
            GetComponentInParent<SpellUI>().OnEquipSpell(spell);
        }
    }

    public void Clear()
    {
        GetComponentInParent<SpellUI>().OnClearSlot(this);
        HideSlot();
    }
}
