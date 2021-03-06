using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestGiver : DialogueTrigger
{
    public Quest quest;
    [SerializeField] QuestEventChannel questEventChannel;

    public override void Interact()
    {
        Transform player = PlayerManager.instance.player;
        transform.rotation = Perception.FaceTarget(transform, player.position, false);

        if (dialogue)
        {
            base.Interact();
            dialogueEventChannel.OnEndDialogue += AssignQuest;

        } else
        {
            AssignQuest();
        }
        
    }

    void AssignQuest()
    {
        if (quest)
        {
            questEventChannel?.RaiseQuestStarted(quest);
            dialogueEventChannel.OnEndDialogue -= AssignQuest;

            dialogue = null;
            quest = null;
        }
    }
}
