using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;

    [SerializeField] GameObject questUI;
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
        Debug.Log("Quest received: " + q.info.title);
    } 

    void MarkCompleted(Quest q)
    {
        if (quests.Remove(q))
        {
            questCompleteDialogue.Init(q.info.title, q.reward.xp);
            dialogueEventChannel.OnStartDialogue(questCompleteDialogue);
            questEventChannel?.RaiseQuestCompleted(q);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            questUI.SetActive(!questUI.activeSelf);
        }
    }
}
