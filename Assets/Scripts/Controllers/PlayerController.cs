using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] LayerMask movementMask;
    [SerializeField] int raycastRange = 1000;
    [SerializeField] float maxTabDistance = 100f;
    [Tooltip("In Degrees")][SerializeField] float fov = 90f; 

    private Interactable focus;

    private Camera cam;
    private PlayerMotor motor;
    private Character thisCharacter;

    public void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        thisCharacter = GetComponent<Character>();
    }

    public void Update()
    {


    }

    public void FocusNextTarget()
    {
        EnemyInteractable[] enemies = FindObjectsOfType<EnemyInteractable>();

        EnemyInteractable closestEnemy = null;
        float closestDistance = maxTabDistance;

        foreach(EnemyInteractable enemy in enemies)
        {
            if (enemy == focus) continue;
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance <= closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
         
        if(closestEnemy != null)
        {
            SetFocus(closestEnemy);
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

    public void ProcessInteraction(Vector3 mousePosition)
    {
        Ray ray = cam.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null && interactable != focus)
            {
                SetFocus(interactable);
            }
            else if(focus != null && interactable == null)
            {
                RemoveFocus();
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
        }

        focus.OnFocused(transform);
    }

    private void RemoveFocus()
    {
        if(focus != null) focus.OnDefocused();
        motor.StopFollowingTarget(); 
        focus = null;
    }

    public bool IsDead()
    {
        return thisCharacter.IsDead();
    }

    public void Move(float vertical, float horizontal)
    {
        motor.Move(vertical, horizontal, cam.transform.forward, cam.transform.right);
    }

    public void StopMoving()
    {
        motor.StopMoving();
    }

    public void Rotate(float amount)
    {
        motor.Rotate(amount);
    }

    public void Attack()
    {
        if(focus != null && IsFacingFocus()) focus.Interact();
    }

    private bool IsFacingFocus()
    {
        if(focus != null)
        {
            Vector3 forward = transform.forward;
            forward.y = 0;
            Vector3 toFocus = focus.transform.position - transform.position;
            toFocus.y = 0;

            float cosAngle = Vector3.Dot(forward, toFocus) / (forward.magnitude * toFocus.magnitude);

            return cosAngle > Mathf.Cos(fov / 2 * Mathf.Deg2Rad); 

        }
        return false;
    }
}