using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimedStat : Stat
{
    public AnimationCurve timeDecrease;

    private float currentValue;

    public void Init()
    {
        TimeManager.onNewHour += OnNewHour;
        TimeManager.onNewDay += OnNewDay;
        OnNewDay();
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
