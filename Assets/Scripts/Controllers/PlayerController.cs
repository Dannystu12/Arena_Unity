using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] LayerMask movementMask;
    [SerializeField] int raycastRange = 1000;
    [SerializeField] float maxTabDistance = 100f;

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

        //if(Input.GetMouseButtonDown(1))
        //{
        //    ProcessInteraction();
        //}
        //else if(Input.GetMouseButton(3)) 
        //{
        //    ProcessMovement();
        //}
        //else if(Input.GetMouseButtonUp(3))
        //{
        //    motor.StopMoving();
        //}
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

    public bool IsDead()
    {
        return thisCharacter.IsDead();
    }

    public void Move(float moveAmount)
    {
        motor.Move(moveAmount);
    }

    public void StopMoving()
    {
        motor.StopMoving();
    }

    public void Rotate(float amount)
    {
        motor.Rotate(amount);
    }
}