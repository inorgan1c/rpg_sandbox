using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region Singleton
    public static QuestManager instance;

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

    public List<Quest> activeQuests;
    public List<Quest> completedQuests;
    public delegate void OnQuestUpdate(Quest.Info questInfo, bool complete);
    public event OnQuestUpdate onQuestUpdateCallback;

    [SerializeField] GameObject questUI;
    [SerializeField] QuestCompleteDialogue questCompleteDialogue;

    private void Start()
    {
    }

    public void AddQuest(Quest q)
    {
        q.Initialize();
        q.questCompleted.AddListener(MarkCompleted);
        activeQuests.Add(q);
        Debug.Log("Quest received: " + q.info.title);
        
        if (onQuestUpdateCallback != null)
        {
            onQuestUpdateCallback.Invoke(q.info, false);
        } 
    } 

    void MarkCompleted(Quest q)
    {
        if (activeQuests.Remove(q))
        {
            questCompleteDialogue.Init(q.info.title, q.reward.xp);
            DialogueManager.StartDialogue(questCompleteDialogue);

            completedQuests.Add(q);
            onQuestUpdateCallback.Invoke(q.info, true);
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
