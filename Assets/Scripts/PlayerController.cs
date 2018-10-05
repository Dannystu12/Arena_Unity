using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float speed = 100f;
    [SerializeField] float rotationSpeed = 80f;
    [SerializeField] float movementDeadzone = 25;


    private CharacterController controller;
    private Animator animator;
    

    private Vector3 targetPosition;
    private Vector3 lookAtTarget;
    private Quaternion rotation;
    private bool moving = false;
    private bool attacking = false;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        ProcessAttack();
        ProcessMovement();
        ProcessAnimation();
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
            moving = false;
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

            lookAtTarget = new Vector3(targetPosition.x - transform.position.x,
                transform.position.y,
                targetPosition.z - transform.position.z);

            rotation = Quaternion.LookRotation(lookAtTarget);
            if (Vector3.Distance(transform.position, targetPosition) < movementDeadzone)
            {
                moving = false;
            }
            else
            {
                moving = true;
            }

        }
    }


    private void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        Vector3 dir = targetPosition - transform.position;
        Vector3 movement = dir.normalized * speed * Time.deltaTime;
        controller.Move(movement);        
    }

    private void ProcessAnimation()
    {
        if (!moving && !attacking && !animator.GetBool("Idle"))
        {
            ResetAnimator();
            animator.SetBool("Idle", true);
        }

        if (!attacking && moving && !animator.GetBool("Walk"))
        {
            ResetAnimator(); 
            animator.SetBool("Walk", true);
        }

     
        if (attacking && !animator.GetBool("Attack"))
        {
            ResetAnimator();
            animator.SetBool("Attack", true);
        }
    }

    private void ProcessAttack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            attacking = true;
        }
    }

    private void ResetAnimator()
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            animator.SetBool(parameter.name, false);
        }
    }

}
