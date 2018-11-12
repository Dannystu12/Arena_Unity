using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private CameraController camController;
    private PlayerController playerController;

	void Start () {
        camController = FindObjectOfType<CameraController>();
        playerController = FindObjectOfType<PlayerController>();
	}
	
	void Update () {

        //Player
        if (!playerController.IsDead())
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                playerController.FocusNextTarget();
            }

            if (Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Epsilon)
            {
                playerController.Move(Input.GetAxis("Vertical"));
            }
            else
            {
                playerController.StopMoving();
            }

            if(Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon)
            {
                camController.Rotate(Input.GetAxis("Horizontal"));
                playerController.Rotate(Input.GetAxis("Horizontal"));
            }

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                playerController.Attack();
            }
        }



        //Camera
        if (Input.GetMouseButtonDown(0))
        {
            playerController.ProcessInteraction(Input.mousePosition);
        }

        if(Input.GetMouseButton(1))
        {
            camController.UpdateRotation(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        //Update zoom
        if(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > Mathf.Epsilon)
        {
            camController.UpdateDistance(Input.GetAxis("Mouse ScrollWheel"));
        }



    }
}
