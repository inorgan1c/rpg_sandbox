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

    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.onDialogueStart += OnDialogueStart;
        DialogueManager.onDialogueEnd += OnDialogueEnd;
        dialoguePanel.SetActive(false);
    }

    void OnDialogueStart(string title)
    {
        if (dialoguePanel)
        {
            dialoguePanel.SetActive(true);
            narratorText.text = title;
            RequestNextLine();

            nextLineButton.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(false);
        }
    }

    public void RequestNextLine()
    {
        dialogueLineText.text = DialogueManager.NextLine();
    }

    void OnDialogueEnd()
    {
        nextLineButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
    }
}
