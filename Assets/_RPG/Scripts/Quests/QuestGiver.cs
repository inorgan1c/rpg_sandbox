using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestGiver : DialogueTrigger
{
    public Quest quest;

    public override void Interact()
    {
        if (dialogue)
        {
            base.Interact();
            DialogueManager.onDialogueEnd += AssignQuest;

        } else
        {
            AssignQuest();
        }
        
    }

    void AssignQuest()
    {
        if (quest)
        {
            QuestManager.instance.AddQuest(quest);
            DialogueManager.onDialogueEnd -= AssignQuest;
            quest = null;
        }
    }
}
