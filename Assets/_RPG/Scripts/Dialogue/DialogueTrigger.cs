using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    [SerializeField] protected Dialogue dialogue;

    public override void Interact()
    {
        base.Interact();
        DialogueManager.StartDialogue(dialogue);
    }
}
