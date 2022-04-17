using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] Transform slotsParent;
    [SerializeField] Text calendar;
    [SerializeField] Text clock;
    [SerializeField] GameObject alertPin;

    [SerializeField] GameObject slotPrefab;
    [SerializeField] GameObject journalPanel;
    [SerializeField] TimeEventChannel timeEventChannel;
    [SerializeField] QuestEventChannel questEventChannel;
    [SerializeField] QuestUIEventChannel questUIEventChannel;


    private void Awake()
    {
        questUIEventChannel.OnJournalToggle += ToggleJournal;
    }

    private void OnDestroy()
    {
        questUIEventChannel.OnJournalToggle -= ToggleJournal;
    }

    private void Start()
    {
        questEventChannel.OnQuestStarted += UpdateJournal;
        questEventChannel.OnQuestCompleted += UpdateJournal;
        timeEventChannel.OnNewDay += UpdateCalendar;
        timeEventChannel.OnNewHour += UpdateClock;

        if (!journalPanel)
        {
            journalPanel = gameObject;
        }

        UpdateCalendar();
        UpdateClock();
        ToggleJournal();
    }


    void ToggleJournal()
    {
        journalPanel.SetActive(!journalPanel.activeSelf);

        alertPin.SetActive(false);
    }


    void UpdateJournal(Quest quest)
    {
        alertPin.SetActive(true);

        if (!quest.completed)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotsParent);
            newSlot.GetComponent<QuestSlot>().SetInfo(quest.info);
            quest.CheckGoals();

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
