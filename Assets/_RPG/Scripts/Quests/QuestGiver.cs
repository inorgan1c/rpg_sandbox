using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestGiver : Interactable
{
    public Quest quest;

    public override void Interact()
    {
        base.Interact();

        if (quest)
        {
            QuestManager.instance.AddQuest(quest);
            quest = null;
        }
    }
}
