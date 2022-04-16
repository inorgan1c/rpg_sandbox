using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform = null;

    bool isFocus = false;
    bool hasInteracted = false;
    Transform player = null;

    public virtual void Interact()
    {
    }

    private void Start()
    {
        if (!interactionTransform)
        {
            interactionTransform = transform;
        }
    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(interactionTransform.position, player.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }


    public void OnFocus(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnFocusLost()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;

    }


    private void OnDrawGizmosSelected()
    {
        if (!interactionTransform)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
