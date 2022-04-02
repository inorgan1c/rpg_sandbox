using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    public delegate void OnDialogueStart(string narrator);
    public static event OnDialogueStart onDialogueStart;
    public delegate void OnDialogueEnd();
    public static event OnDialogueEnd onDialogueEnd;

    #region Singleton
    public static DialogueManager instance;

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

    public void Start()
    {
        sentences = new Queue<string>();
    }

    public static void StartDialogue(Dialogue dialogue)
    {
        instance.sentences.Clear();
        if (dialogue)
        {
            foreach (string line in dialogue.dialogueLines)
            {
                instance.sentences.Enqueue(line);
            }
            onDialogueStart?.Invoke(dialogue.narrator);
        
        } else
        {
            EndDialogue();
        }
    }

    public static string NextLine()
    {
        string sentence = "";
        if (instance.sentences.Count == 0)
        {
            EndDialogue();
        } else
        {
            sentence = instance.sentences.Dequeue();
        }

        return sentence;
    }

    public static void EndDialogue()
    {
        onDialogueEnd?.Invoke();
    }
}
