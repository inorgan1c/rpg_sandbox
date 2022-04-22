using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ManaStat : TimedStat
{
    protected override void OnNewHour()
    {
        base.OnNewHour();

        currentValue += timeModifier.Evaluate(PlayerStats.awakenTime);
        currentValue = Mathf.Clamp(currentValue, 0, baseValue);
        StatsChannel.RaiseManaChanged(charID, GetBaseValue(), GetValue());
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
