using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text narratorText;
    public Text dialogueLineText;
    public Button nextLineButton;
    public Button closeButton;

    public DialogueEventChannel dialogueEventChannel;

    private Queue<string> sentences;

    private void OnEnable()
    {
        dialogueEventChannel.OnStartDialogue += StartDialogue;
    }

    private void OnDisable()
    {
        dialogueEventChannel.OnStartDialogue -= StartDialogue;

    }

    void Start()
    {
        dialoguePanel.SetActive(false);
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        if (dialogue)
        {
            foreach (string line in dialogue.dialogueLines)
            {
                sentences.Enqueue(line);
            }
            Show(dialogue.narrator);
        }
        else
        {
            EndDialogue();
        }
    }

    public void NextLine()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            dialogueLineText.text = sentences.Dequeue();

        }
    }

    public void EndDialogue()
    {
        nextLineButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
        dialogueEventChannel?.RaiseEndDialogue();
    }

    void Show(string title)
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
            narratorText.text = title;
            NextLine();

            nextLineButton.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(false);
        }
    }
}
