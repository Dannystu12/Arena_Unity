using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float speed = 100f;
    [SerializeField] float rotationSpeed = 80f;
    [SerializeField] float movementDeadzone = 25;


    private CharacterController controller;
    private CharacterAnimator characterAnimator;

    private Vector3 targetPosition;
    private Vector3 lookAtTarget;
    private Quaternion rotation;
    private bool moving = false; 

    public event System.Action OnAttack;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        characterAnimator = GetComponent<CharacterAnimator>();
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
            moving = false;
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

    public bool IsMoving()
    {
        return moving;
    }
}
