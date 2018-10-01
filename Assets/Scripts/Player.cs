using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float moveAmount = 100f;

    CharacterController controller;
    Vector3 cameraDelta;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        cameraDelta = Camera.main.transform.position - transform.position;
	}
	
	// Update is called once per frame 
	void Update () {
        Move();
        
	}

    private void Move()
    {

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection *= moveAmount;
        controller.Move(moveDirection * Time.deltaTime);
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 newPos = transform.position;
        Camera.main.transform.position = newPos + cameraDelta;
    }
}
