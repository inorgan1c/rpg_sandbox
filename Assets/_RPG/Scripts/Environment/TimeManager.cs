using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Singleton
    public static TimeManager instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    public float fullDayLength;
    public float startTime = 0.4f;
    private float timeRate;
    private float lastHour;
    public static float time { get; private set; }

    public delegate void OnNewDay();
    public static event OnNewDay onNewDay;
    public delegate void OnNewHour();
    public static event OnNewHour onNewHour;

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
            onNewDay?.Invoke();
        }

        if (hours != lastHour)
        {
            onNewHour?.Invoke();
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
