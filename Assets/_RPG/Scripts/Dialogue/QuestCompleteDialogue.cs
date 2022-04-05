using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Complete Message", menuName = "Dialogue/Default Quest Message")]
public class QuestCompleteDialogue : Dialogue
{
    public void Init(string questName, int reward)
    {
        dialogueLines = new string[2];
        dialogueLines[0] = questName + ": Quest Completed!";
        dialogueLines[1] = "Received: " + reward + "xp";
    }
}
