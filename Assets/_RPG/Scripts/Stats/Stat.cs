using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    protected int baseValue;

    private List<int> modifiers = new List<int>();

    public virtual int GetValue()
    {
        int value = baseValue;
        modifiers.ForEach(m => value += m);

        return value;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}
 
