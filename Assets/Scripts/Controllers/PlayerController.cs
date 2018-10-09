using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] LayerMask movementMask;
    [SerializeField] int raycastRange = 1000;

    private Interactable focus;

    private Camera cam;
    private PlayerMotor motor;

    public void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            ProcessInteraction();
        }
        else if(Input.GetMouseButton(0)) 
        {
            ProcessMovement();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            motor.StopMoving();
        }   
    }

    private void ProcessMovement()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit, raycastRange, movementMask))
        {
            motor.MoveToPoint(hit.point);
            RemoveFocus();
        }
    }

    private void ProcessInteraction()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                SetFocus(interactable);
            }
        }
    }

    private void SetFocus(Interactable newFocus)
    {
        //Check if already have a focus then defocus
        if(newFocus != focus)
        {
            if(focus != null) focus.OnDefocused();
            focus = newFocus;
            motor.FollowTarget(focus);
        }

        focus.OnFocused(transform);
    }

    private void RemoveFocus()
    {
        if(focus != null) focus.OnDefocused();
        motor.StopFollowingTarget(); 
        focus = null;
    }
}