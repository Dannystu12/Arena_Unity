using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour {

    [SerializeField] float speed = 100.0f;
    [SerializeField] Vector3 cameraOffset;

    Animator animator;
    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        cameraOffset = this.transform.position - Camera.main.transform.position;
        rigidBody = transform.GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        SetAnimation();
        Move();
        TrackCamera();  
	}

    private void SetAnimation()
    {
        if (Input.GetAxis("Horizontal") != 0||
            Input.GetAxis("Vertical") != 0)
        {
            if(!animator.GetBool("Run"))
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);
            }

        }
        else
        {
            if(!animator.GetBool("Idle"))
            {
                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
            }
        } 
    }

    private void Move()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput != 0)
        {
            rigidBody.AddForce(new Vector3(0, 0, speed * verticalInput), ForceMode.VelocityChange);
            rigidBody.rotation = Quaternion.LookRotation(Vector3.forward * verticalInput);
        }
        else if (horizontalInput != 0)
        {
            rigidBody.AddForce(new Vector3(speed * horizontalInput, 0, 0), ForceMode.VelocityChange);
            rigidBody.rotation = Quaternion.LookRotation(Vector3.right * horizontalInput);
        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void TrackCamera()
    {
        Camera.main.transform.position = transform.position - cameraOffset;
    }
}
