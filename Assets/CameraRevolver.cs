using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRevolver : MonoBehaviour {

    [SerializeField] float rotationSpeed = 0.25f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, rotationSpeed, 0);
    }
}
