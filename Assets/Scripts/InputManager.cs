using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private CameraController camController;
    private PlayerController playerController;
    private Joystick JoystickL;
    private Joystick JoystickR;

	void Start () {
        camController = FindObjectOfType<CameraController>();
        playerController = FindObjectOfType<PlayerController>();
        JoystickL = GameObject.FindGameObjectWithTag("Left Joystick").GetComponent<Joystick>();
        JoystickR = GameObject.FindGameObjectWithTag("Right Joystick").GetComponent<Joystick>();
	}
	
	void Update () {

        //Player
        if (!playerController.IsDead())
        {

            if (Mathf.Abs(JoystickL.Horizontal) > Mathf.Epsilon
                || Mathf.Abs(JoystickL.Vertical) > Mathf.Epsilon)
            {
                playerController.Move(JoystickL.Vertical, JoystickL.Horizontal);
            }
            else
            {
                playerController.StopMoving();
            }

            if(Input.GetKeyDown(KeyCode.Alpha1)) // TODO Change to touch attack Button
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
