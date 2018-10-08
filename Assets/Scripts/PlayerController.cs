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
        if(Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                // Check if we hit an enemy
                // set enemy as focus

            }
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


        }
    }


    private void Move()
    {
        agent.SetDestination(targetPosition);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            StopMoving();
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

}
