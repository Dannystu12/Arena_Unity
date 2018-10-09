using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    [Header("Tracking Config")]
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float pitch;

    [Header("Zoom Config")]
    [SerializeField] float zoomSpeed = 4f;
    [SerializeField] float minZoom = 5f;
    [SerializeField] float maxZoom = 40f;
    [SerializeField] float currentZoom = 15f;

    [Header("Yaw Config")]
    [SerializeField] float yawSpeed = 100f;
    [SerializeField] float currentYaw = 0f;

    private void Update()
    {
        UpdateZoom();
        UpdateYaw();
    }

    private void UpdateZoom()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    private void UpdateYaw()
    {
        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {
        //Track player
        if(target.position != null)
        {
            transform.position = target.position - offset * currentZoom;
            transform.LookAt(target.position + Vector3.up * pitch);

            //Rotate camera
            transform.RotateAround(target.position, Vector3.up, currentYaw);
        }

    }
}
