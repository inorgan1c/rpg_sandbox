using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Dialogue Event Channel", menuName = "Event/Dialogue Event Channel")]
public class DialogueEventChannel : ScriptableObject
{
    public UnityAction<Dialogue> OnStartDialogue;
    public UnityAction OnEndDialogue;
    

    public void RaiseStartDialogue(Dialogue dialogue)
    {
        OnStartDialogue?.Invoke(dialogue);
    }

    public void RaiseEndDialogue()
    {
        OnEndDialogue?.Invoke();
    }
}
