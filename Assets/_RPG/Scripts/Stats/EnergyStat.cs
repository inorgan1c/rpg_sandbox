using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnergyStat : TimedStat
{
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }


    protected override void OnNewHour()
    {
        base.OnNewHour();

        float modifier = timeModifier.Evaluate(PlayerStats.awakenTime);
        if (isSleeping)
        {
            modifier *= -1f;
        }

        currentValue += modifier;
        currentValue = Mathf.Clamp(currentValue, 0, baseValue);
        StatsChannel.RaiseEnergyChanged(charID, GetBaseValue(), GetValue());
    }
}
