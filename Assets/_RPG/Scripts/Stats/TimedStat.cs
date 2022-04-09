using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimedStat : Stat
{
    public AnimationCurve timeDecrease;

    [SerializeField] TimeEventChannel TimeChannel;
    private float currentValue;

    public void Init()
    {
        TimeChannel.OnNewHour += OnNewHour;
        TimeChannel.OnNewDay += OnNewDay;
        OnNewDay();
    }

    public void Restore(int p)
    {
        currentValue += p;
        if (p > baseValue)
        {
            p = baseValue;
        }
    }

    void OnNewHour()
    {
        currentValue -= timeDecrease.Evaluate(PlayerStats.awakenTime);
        if (currentValue <= 0)
        {
            currentValue = 0;
        }
    }

    void OnNewDay()
    {
        currentValue = baseValue;
    }

    public override int GetValue()
    {
        return Mathf.RoundToInt(currentValue);
    }
}
