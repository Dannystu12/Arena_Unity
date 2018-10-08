using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    [SerializeField] float speed = 100f;
    [SerializeField] float rotationSpeed = 80f;
    [SerializeField] float movementDeadzone = 100;
    [SerializeField] LayerMask movementMask;


    private CharacterController controller;
    private CharacterAnimator characterAnimator;
    private NavMeshAgent agent;
    private Enemy focus;

    private Vector3 targetPosition;
    private Vector3 lookAtTarget;
    private Quaternion rotation;
    private bool moving = false; 

    public event System.Action OnAttack;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        characterAnimator = GetComponent<CharacterAnimator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (characterAnimator.IsLocked()) return;
        ProcessAttack();
        ProcessMovement();
	}

    private void ProcessAttack()
    {
        if(Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if(enemy != null)
                {
                    SetFocus(enemy);
                    moving = true;
                }
                
            }
            //StopMoving();
            //if(OnAttack != null) OnAttack();
        }
    }

    private void ProcessMovement()
    {
        if (Input.GetMouseButton(0))
        {
            RemoveFocus();
            SetTargetPosition();
            if (moving) MoveTowards(targetPosition);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            StopMoving();
        }
        else if(focus != null)
        {
           MoveTowards(focus.transform.position);
        }
    } 

    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            targetPosition = hit.point;
            
            targetPosition.y = transform.position.y;

            moving = true;

        }
    }


    public bool IsMoving()
    {
        return moving;
    }

    private void StopMoving()
    {
        moving = false;
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }

    private void SetFocus(Enemy enemy)
    {
        focus = enemy;
    }

    private void RemoveFocus()
    {
        focus = null;
    }

    private void MoveTowards(Vector3 point)
    {
        agent.SetDestination(point);
        FaceTarget(point);
        agent.isStopped = false;

        if (Vector3.Distance(transform.position, point) <= agent.stoppingDistance)
        {
            StopMoving();
        }
    }

    private void FaceTarget(Vector3 point)
    {
        Vector3 direction = (point - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
