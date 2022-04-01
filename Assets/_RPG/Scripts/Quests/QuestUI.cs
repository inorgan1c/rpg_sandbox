using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public Transform slotsParent;

    [SerializeField] GameObject slotPrefab;
    QuestManager questManager;

    private void Start()
    {
        questManager = QuestManager.instance;
        questManager.onQuestUpdateCallback += UpdateJournal;
        gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
          
    }

    void UpdateJournal(Quest.Info questInfo, bool complete)
    {
        if (!complete)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotsParent);
            newSlot.GetComponent<QuestSlot>().SetInfo(questInfo);
        } else
        {
            QuestSlot[] slots = slotsParent.GetComponentsInChildren<QuestSlot>();
            foreach (QuestSlot slot in slots)
            {
                if (slot.info.title == questInfo.title)
                {
                    slot.MarkCompleted();
                }
            }
        }
    }
}
