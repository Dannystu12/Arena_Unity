using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] LayerMask movementMask;
    [SerializeField] int raycastRange = 1000;

    Camera cam;
    PlayerMotor motor;

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
        }
    }

    private void ProcessInteraction()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            // Check if hit interactable
        }
    }
}