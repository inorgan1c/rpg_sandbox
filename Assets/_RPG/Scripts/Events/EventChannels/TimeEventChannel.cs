using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Time Event Channel", menuName = "Event/Time Event Channel")]
public class TimeEventChannel : ScriptableObject
{
    public UnityAction OnNewDay;
    public UnityAction OnNewHour;


    public void RaiseNewDay()
    {
        OnNewDay?.Invoke();
    }

    public void RaiseNewHour()
    {
        OnNewHour?.Invoke();
    }
}
