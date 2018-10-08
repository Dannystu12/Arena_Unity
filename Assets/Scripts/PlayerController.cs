using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    [SerializeField] float speed = 100f;
    [SerializeField] float rotationSpeed = 80f;
    [SerializeField] float movementDeadzone = 25;
    [SerializeField] LayerMask movementMask;


    private CharacterController controller;
    private CharacterAnimator characterAnimator;
    private NavMeshAgent agent;

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
    }
	
	// Update is called once per frame
	void Update () {
        if (characterAnimator.IsLocked()) return;
        ProcessAttack();
        ProcessMovement();
	}

    private void ProcessAttack()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StopMoving();
            if(OnAttack != null) OnAttack();
        }
    }

    private void ProcessMovement()
    {
        if (Input.GetMouseButton(0))
        {
            SetTargetPosition();
            if (moving) Move();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            StopMoving();
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
            agent.isStopped = false;


            if ((targetPosition - transform.position).magnitude <= movementDeadzone)
            {
                StopMoving();
            }


        }
    }


    private void Move()
    {
        Vector3 movementVector = (targetPosition - transform.position).normalized * speed * Time.deltaTime;
        agent.Move(movementVector );

        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
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
}
