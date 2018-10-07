using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCamera : MonoBehaviour {

    [SerializeField] float zoomSpeed = 4f;
    [SerializeField] float minZoom = 5f;
    [SerializeField] float maxZoom = 40f;
    [SerializeField] float currentZoom = 15f;


    Player player;
    Vector3 offset;
    Camera camera;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        offset = player.transform.position - gameObject.transform.position;
        camera = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateZoom();
        TrackPlayer();
	}

    private void TrackPlayer()
    {
        gameObject.transform.position = player.transform.position - offset;
    }

    private void UpdateZoom()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        camera.fieldOfView = currentZoom;
    }
}
