using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    [System.Serializable]
    public struct Info
    {
        public string title;
        public string description;
    }

    [System.Serializable]
    public struct Stat
    {
        public int xp;
        public int gold;
    }

    public abstract class QuestGoal : ScriptableObject
    {
        protected string description;
        public int CurrentAmount { get; protected set; }
        public int requiredAmount = 1;
        public bool Completed { get; protected set; }
        [HideInInspector] public UnityEvent QuestGoalCompleted;

        public virtual string Description()
        {
            return description;
        }

        public virtual void Initialize()
        {
            Completed = false;
            QuestGoalCompleted = new UnityEvent();
        }

        protected void Evaluate()
        {
            if (CurrentAmount >= requiredAmount)
            {
                Complete();
            }
        }

        private void Complete()
        {
            Completed = true;
            QuestGoalCompleted.Invoke();
            QuestGoalCompleted.RemoveAllListeners();
        }
    }


    public Info info;
    public Stat reward;
    public List<QuestGoal> goals;
    public bool completed = false;
    public QuestCompletedEvent questCompleted;

    public void Initialize()
    {
        completed = false;
        questCompleted = new QuestCompletedEvent();

        foreach (QuestGoal goal in goals)
        {
            goal.Initialize();
            goal.QuestGoalCompleted.AddListener( delegate { CheckGoals(); });
        }
    }

    private void CheckGoals()
    {
        completed = goals.TrueForAll(goal => goal.Completed);
        if (completed)
        {
            //reward ... 
            questCompleted.Invoke(this);
            questCompleted.RemoveAllListeners();
        }
    }
}


public class QuestCompletedEvent : UnityEvent<Quest> { }
