using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 targetOffset = new Vector3(0f, 5f, 0f);
    [SerializeField] float distance = 10.0f;
    private float currentX = 0f;
    private float currentY = 0f;
    [SerializeField] Vector3 offset = new Vector3(0.0f, 250f, 0.0f); 
    [SerializeField] float sensitivityX = 4.0f;
    [SerializeField] float sensitivityY = 1.0f;
    [SerializeField] float yAngleMin = -50f;
    [SerializeField] float yAngleMax = 50.0f;
    [SerializeField] float zoomRate = 20.0f;
    [SerializeField] float minDistance = 2.5f;
    [SerializeField] float maxDistance = 20f;

    private Camera cam;
    private Transform camTransform;
   


    void Start()
    {
        camTransform = transform;
        cam = Camera.main;
        Vector3 angles = transform.eulerAngles;
        currentX = angles.x;
        currentY = angles.y;
    }

    private void Update()
    {
        transform.position = target.position;
        if(Input.GetMouseButton(0))
        {
            
            currentX += Input.GetAxis("Mouse X");
            currentY -= Input.GetAxis("Mouse Y");
            currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);


        }

        distance += -(Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 position = rotation * new Vector3(0.0f, 2.0f, -distance) + target.position;

        transform.rotation = rotation;
        transform.position = position + offset;


         

    }


    void LateUpdate()
    {
        //Vector3 dir = new Vector3(0, 0, -distance);
        //Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        //camTransform.position = target.position + targetOffset + rotation * dir;
        //camTransform.LookAt(target.position);
    }
} 

