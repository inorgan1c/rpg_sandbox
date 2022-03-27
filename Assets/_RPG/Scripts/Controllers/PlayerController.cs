using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;
    public Interactable currentFocus;

    Camera cam;
    PlayerMotor motor;

    

    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                //move to world position if walkable
                if (Physics.Raycast(ray, out hit, 100, movementMask))
                {
                    motor.MoveToPoint(hit.point);
                    RemoveFocus();
                }

            }

            if (Input.GetMouseButton(1))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                //set focus on hit if interactable
                if (Physics.Raycast(ray, out hit))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable)
                    {
                        SetFocus(interactable);
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
}
