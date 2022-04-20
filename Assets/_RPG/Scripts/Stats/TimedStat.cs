using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimedStat : Stat
{
    public AnimationCurve timeModifier;

    [SerializeField] protected TimeEventChannel TimeChannel;
    protected float currentValue;

    public virtual void Init()
    {
        TimeChannel.OnNewHour += OnNewHour;
    }
    
    protected virtual void OnDestroy()
    {
        TimeChannel.OnNewHour -= OnNewHour;
    }

    public void Restore(int p)
    {
        currentValue += p;
        if (p > baseValue)
        {
            currentValue = baseValue;
        }
    }

    protected virtual void OnNewHour()
    {
        
    }

    public override int GetValue()
    {
        return Mathf.RoundToInt(currentValue);
    }
}
