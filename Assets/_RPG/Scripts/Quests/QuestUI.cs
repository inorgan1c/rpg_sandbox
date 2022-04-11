using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public Transform slotsParent;
    public Text calendar;
    public Text clock;

    [SerializeField] GameObject slotPrefab;
    [SerializeField] TimeEventChannel timeEventChannel;
    [SerializeField] QuestEventChannel questEventChannel;

    private void Start()
    {
        questEventChannel.OnQuestStarted += UpdateJournal;
        questEventChannel.OnQuestCompleted += UpdateJournal;
        timeEventChannel.OnNewDay += UpdateCalendar;
        timeEventChannel.OnNewHour += UpdateClock;

        gameObject.SetActive(false);
    }


    void UpdateJournal(Quest quest)
    {

        if (!quest.completed)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotsParent);
            newSlot.GetComponent<QuestSlot>().SetInfo(quest.info);
            quest.EvaluateGoals();

        } else
        {
            QuestSlot[] slots = slotsParent.GetComponentsInChildren<QuestSlot>();
            foreach (QuestSlot slot in slots)
            {
                if (slot.info.title == quest.info.title)
                {
                    slot.MarkCompleted();
                }
            }
        }
    }

    void UpdateCalendar()
    {
        calendar.text = "Days: " + TimeManager.ElapsedDays();
    }


    void UpdateClock()
    {
        int dayTimeInHours = (int)TimeManager.DayTimeInHours();
        int clockTime = dayTimeInHours % 12;
        string period = dayTimeInHours >= 12 ? "PM" : "AM";
        clock.text = clockTime.ToString("00") + ":00 " + period;
    }
}
