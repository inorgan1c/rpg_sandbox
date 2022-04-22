using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;

    [SerializeField] QuestCompleteDialogue questCompleteDialogue;
    [SerializeField] QuestEventChannel questEventChannel;
    [SerializeField] DialogueEventChannel dialogueEventChannel;

    private void OnEnable()
    {
        questEventChannel.OnQuestStarted += AddQuest;
        questEventChannel.OnQuestCompleted += MarkCompleted;
    }

    private void OnDisable()
    {
        questEventChannel.OnQuestStarted -= AddQuest;
        questEventChannel.OnQuestCompleted -= MarkCompleted;
    }

    public void AddQuest(Quest q)
    {
        q.Initialize();
        quests.Add(q);
    } 

    void MarkCompleted(Quest q)
    {
        if (quests.Remove(q))
        {
            questCompleteDialogue.Init(q.info.title, q.reward.xp);
            dialogueEventChannel.OnStartDialogue(questCompleteDialogue);
        }
    }
}
