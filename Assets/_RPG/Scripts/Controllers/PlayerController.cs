using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;
    public Interactable currentFocus;

    [SerializeField] DialogueEventChannel dialogueChannel;
    [SerializeField] StatsEventChannel statsEventChannel;

    Camera cam;
    PlayerMotor motor;
    bool canMove = true;
    SpellSystem spellSystem;

    private void OnEnable()
    {
        dialogueChannel.OnStartDialogue += OnDialogue;
        dialogueChannel.OnEndDialogue += EnableMovement;
        statsEventChannel.OnSleep += DisableMovement;
        statsEventChannel.OnWakeUp += EnableMovement;
    }

    private void OnDisable()
    {
        dialogueChannel.OnStartDialogue -= OnDialogue;
        dialogueChannel.OnEndDialogue -= EnableMovement;
        statsEventChannel.OnSleep -= DisableMovement;
        statsEventChannel.OnWakeUp -= EnableMovement;
    }

    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        spellSystem = GetComponent<SpellSystem>();
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && canMove)
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
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                { 
                    //set focus on hit if interactable
                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    if (enemy)
                    {
                        spellSystem.Cast(enemy);
                    }
                }
                    
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

    void OnDialogue(Dialogue dialogue)
    {
        DisableMovement();
    }

    void EnableMovement()
    {
        canMove = true;
    }

    void DisableMovement()
    {
        canMove = false;
    }
}


