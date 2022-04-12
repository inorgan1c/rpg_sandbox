using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New QuestUI Event Channel", menuName = "Event/QuestUI Event Channel")]
public class QuestUIEventChannel : ScriptableObject
{
    public UnityAction OnJournalToggle;

    public void RaiseJournalToggle()
    {
        OnJournalToggle?.Invoke();
    }
}
