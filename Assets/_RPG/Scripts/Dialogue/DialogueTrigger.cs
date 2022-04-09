using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    [SerializeField] protected Dialogue dialogue;
    [SerializeField] protected DialogueEventChannel dialogueEventChannel;

    public override void Interact()
    {
        base.Interact();
        dialogueEventChannel?.RaiseStartDialogue(dialogue);
    }
}
