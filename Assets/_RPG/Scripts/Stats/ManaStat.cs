using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ManaStat : TimedStat
{
    public override void Init(TimeEventChannel timeEventChannel)
    {
        base.Init(timeEventChannel);

        currentValue = baseValue;
    }

    protected override void OnNewHour()
    {
        base.OnNewHour();

        currentValue += timeModifier.Evaluate(PlayerStats.awakenTime);
        if (currentValue >= baseValue)
        {
            currentValue = baseValue;
        }
    }

    public void Use(float mp)
    {
        if (currentValue >= mp)
        {
            currentValue -= mp;
        }
    }

    public void Restore(float mp)
    {
        currentValue = Mathf.Clamp(currentValue + mp, 0, baseValue);
    }
}
