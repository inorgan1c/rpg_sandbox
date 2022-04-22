using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimedStat : Stat
{
    public AnimationCurve timeModifier;

    protected TimeEventChannel TimeChannel;
    protected StatsEventChannel StatsChannel;
    protected float currentValue;
    protected bool isSleeping;
    protected int charID;
    public virtual void Init(int id, TimeEventChannel timeEventChannel, StatsEventChannel statsEventChannel)
    {
        currentValue = baseValue;

        TimeChannel = timeEventChannel;
        TimeChannel.OnNewHour += OnNewHour;

        StatsChannel = statsEventChannel;
        StatsChannel.OnSleep += OnSleep;
        StatsChannel.OnWakeUp += OnWakeUp;
        isSleeping = false;

        charID = id;
    }
    
    protected virtual void OnDestroy()
    {
        StatsChannel.OnSleep -= OnSleep;
        StatsChannel.OnWakeUp -= OnWakeUp;
        TimeChannel.OnNewHour -= OnNewHour;
    }

    public virtual void Restore(int p)
    {
        currentValue += p;
        currentValue = Mathf.Clamp(currentValue, 0, baseValue);
    }

    protected virtual void OnNewHour()
    {
        
    }

    protected virtual void OnSleep()
    {
        isSleeping = true;
    }

    protected virtual void OnWakeUp()
    {
        isSleeping = false;
    }

    public override int GetValue()
    {
        return Mathf.RoundToInt(currentValue);
    }
}
