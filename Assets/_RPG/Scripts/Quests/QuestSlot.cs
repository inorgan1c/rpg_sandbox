using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    public Quest.Info info;
    [SerializeField] Text titleTextArea;
    [SerializeField] Text descriptionTextArea;

    public void SetInfo(Quest.Info questInfo)
    {
        info = questInfo;
        titleTextArea.text = info.title;
        descriptionTextArea.text = info.description;
    }

    public void MarkCompleted()
    {
        GetComponentInChildren<Button>().interactable = false;
    }
}
