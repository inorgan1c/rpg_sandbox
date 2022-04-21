using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnergyStat : TimedStat
{
    public override void Init(TimeEventChannel timeEventChannel)
    {
        base.Init(timeEventChannel);

        currentValue = baseValue;

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

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
