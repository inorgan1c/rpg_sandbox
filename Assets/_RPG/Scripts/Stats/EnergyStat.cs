using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnergyStat : TimedStat
{
    public override void Init()
    {
        base.Init();

        TimeChannel.OnNewDay += OnNewDay;
        OnNewDay();

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        TimeChannel.OnNewDay -= OnNewDay;
    }


    void OnNewDay()
    {
        currentValue = baseValue;
    }

    protected override void OnNewHour()
    {
        base.OnNewHour();

        currentValue += timeModifier.Evaluate(PlayerStats.awakenTime);
        if (currentValue <= 0)
        {
            currentValue = 0;
        }
    }
}
