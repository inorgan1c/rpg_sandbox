using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;
    public Interactable currentFocus;
    public DialogueEventChannel DialogueChannel;


    Camera cam;
    PlayerMotor motor;
    bool canMove = false;

    private void OnEnable()
    {
        DialogueChannel.OnStartDialogue += EnableMovement;
        DialogueChannel.OnEndDialogue += DisableMovement;
    }

    private void OnDisable()
    {
        DialogueChannel.OnStartDialogue -= EnableMovement;
        DialogueChannel.OnEndDialogue -= DisableMovement;
    }

    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && !canMove)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //set focus on hit if interactable
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable)
                    {
                        SetFocus(interactable);

                    }

                    //move to point
                    else
                    { 
                        motor.MoveToPoint(hit.point);
                        RemoveFocus();
                    }
                }
            }
           
             
            //cast a magic spell if equipped?
            if (Input.GetMouseButton(1))
            {
                
            }
        }
    }

    void SetFocus(Interactable focus)
    {
        if (focus != currentFocus)
        {
            if (currentFocus)
            {
                currentFocus.OnFocusLost();
            }
            currentFocus = focus;
            motor.FollowTarget(currentFocus);
        }

        focus.OnFocus(transform);
    }

    void RemoveFocus()
    {
        if (currentFocus)
        {
            currentFocus.OnFocusLost();
        }
        motor.StopFollowingTarget();
        currentFocus = null;
    }

    void EnableMovement(Dialogue dialogue=null)
    {
        canMove = true;
    }

    void DisableMovement()
    {
        canMove = false;
    }
}


