using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Quest Event Channel", menuName = "Event/Quest Event Channel")]
public class QuestEventChannel : ScriptableObject
{
    public UnityAction<Quest> OnQuestStarted;
    public UnityAction<Quest> OnQuestCompleted;
    public UnityAction OnQuestGoalCompleted;

    public void RaiseQuestStarted(Quest quest)
    {
        OnQuestStarted?.Invoke(quest);
    }

    public void RaiseQuestCompleted(Quest quest)
    {
        OnQuestCompleted?.Invoke(quest);
    }

    public void RaiseQuestGoalCompleted()
    {
        OnQuestGoalCompleted?.Invoke();
    }

}
