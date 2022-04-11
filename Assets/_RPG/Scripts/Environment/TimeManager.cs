using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public float fullDayLength;
    public float startTime = 0.4f;
    private float timeRate;
    private float lastHour;
    public static float time { get; private set; }

    public TimeEventChannel timeEventChannel;

    // Start is called before the first frame update
    void Start()
    {
        timeRate = 1f / fullDayLength;
        time = startTime;
        lastHour = DayTimeInHours();
    }

    // Update is called once per frame
    void Update()
    {
        time += timeRate * Time.deltaTime;

        float hours = DayTimeInHours();
        if (hours == 0)
        {
            timeEventChannel?.RaiseNewDay();
        }

        if (hours != lastHour)
        {
            timeEventChannel?.RaiseNewHour();
            lastHour = hours;
        }
    }

    public static float DayTimeInHours()
    {
        return Mathf.Floor(DayTime()*24f);
    }

    public static float DayTime()
    {
        return time%1f;
    }


    public static int ElapsedDays()
    {
        return (int)Mathf.FloorToInt(time);
    }
}
