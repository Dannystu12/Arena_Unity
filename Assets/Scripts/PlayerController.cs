using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float speed = 100f;
    [SerializeField] float rotationSpeed = 80f;
    [SerializeField] float movementDeadzone = 25;


    private CharacterController controller;
    private Animator animator;
    

    Vector3 targetPosition;
    Vector3 lookAtTarget;
    Quaternion rotation;
    private bool moving = false;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        ProcessMovement();

        //if (Input.GetMouseButtonDown(0))
        //{
        //    animator.SetBool("Walk", false);
        //    animator.SetBool("Idle", false);
        //    animator.SetBool("Attack", true);
        //}
        //else if (Input.GetKeyDown(KeyCode.W)) 
        //{
        //    animator.SetBool("Walk", true);
        //    animator.SetBool("Idle", false);
        //    animator.SetBool("Attack", false);
        //}

        //if (Input.GetKey(KeyCode.W))
        //{
        //    moveDirection = Vector3.forward ;
        //    moveDirection *= speed ;
        //    moveDirection = transform.TransformDirection(moveDirection);
        //}

        //if(Input.GetKeyUp(KeyCode.W) || Input.GetMouseButtonUp(0))
        //{
        //    moveDirection = Vector3.zero;
        //    animator.SetBool("Walk", false);
        //    animator.SetBool("Idle", true);
        //    animator.SetBool("Attack", false);
        //}

        //rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        //transform.eulerAngles = new Vector3(0, rotation, 0);
        //controller.Move(moveDirection * Time.deltaTime);
	}

    private void ProcessMovement()
    {
        if (Input.GetMouseButton(0))
        {
            SetTargetPosition();
            if (moving) Move();
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
        //transform.position = Vector3.MoveTowards(transform.position,
        //    targetPosition,
        //    speed * Time.deltaTime);

        
    }
}
