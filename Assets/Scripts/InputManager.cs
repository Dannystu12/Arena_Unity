using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private CameraController camController;
    private PlayerController playerController;
    private Joystick JoystickL;
    private Joystick JoystickR;
    private RectTransform AttackButtonRT;


	void Start () {
        camController = FindObjectOfType<CameraController>();
        playerController = FindObjectOfType<PlayerController>();
        JoystickL = GameObject.FindGameObjectWithTag("Left Joystick").GetComponent<Joystick>();
        JoystickR = GameObject.FindGameObjectWithTag("Right Joystick").GetComponent<Joystick>();
        AttackButtonRT = GameObject.FindGameObjectWithTag("Attack Button").GetComponent<RectTransform>();
    }
	
	void Update () {

        //Player
        if (!playerController.IsDead())
        {

            //Movement
            if (Mathf.Abs(JoystickL.Horizontal) > Mathf.Epsilon
                || Mathf.Abs(JoystickL.Vertical) > Mathf.Epsilon)
            {
                playerController.Move(JoystickL.Vertical, JoystickL.Horizontal);
            }
            else
            {
                playerController.StopMoving();
            }

            //Targeting
            if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began) )
            {
                //Ensure touch is not on joysticks or attack button before processing interaction
                RectTransform rectL = JoystickL.GetComponent<RectTransform>();
                RectTransform rectR = JoystickR.GetComponent<RectTransform>();
                if (!RectTransformUtility.RectangleContainsScreenPoint(rectL, Input.GetTouch(0).position)
                    && !RectTransformUtility.RectangleContainsScreenPoint(rectR, Input.GetTouch(0).position)
                    && !RectTransformUtility.RectangleContainsScreenPoint(AttackButtonRT, Input.GetTouch(0).position))
                {
                    playerController.ProcessInteraction(Input.GetTouch(0).position);
                }

            }


            //Camera Rotation
            if (Mathf.Abs(JoystickR.Horizontal) > Mathf.Epsilon
                || Mathf.Abs(JoystickR.Vertical) > Mathf.Epsilon)
            {
                camController.UpdateRotation(JoystickR.Horizontal, JoystickR.Vertical);
            }

            //Camera Zoom
            if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > Mathf.Epsilon)
            {
                camController.UpdateDistance(Input.GetAxis("Mouse ScrollWheel"));
            }
        }
    }

    public void Attack()
    {
        if(!playerController.IsDead())
        {
            playerController.Attack();
        }
    }
}
