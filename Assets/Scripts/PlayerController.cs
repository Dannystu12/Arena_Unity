using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float speed = 100f;
    [SerializeField] float rotationSpeed = 80f;

    Vector3 moveDirection = Vector3.zero;
    float rotation = 0;

    CharacterController controller;
    Animator animator;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Attack", true);
        }
        else if (Input.GetKeyDown(KeyCode.W)) 
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Idle", false);
            animator.SetBool("Attack", false);
        }

        



        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = Vector3.forward ;
            moveDirection *= speed ;
            moveDirection = transform.TransformDirection(moveDirection);
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetMouseButtonUp(0))
        {
            moveDirection = Vector3.zero;
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Attack", false);
        }

        rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotation, 0);
        controller.Move(moveDirection * Time.deltaTime);
	}
}
