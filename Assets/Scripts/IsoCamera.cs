using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCamera : MonoBehaviour {

    Player player;
    Vector3 offset;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        offset = player.transform.position - gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = player.transform.position - offset;
	}
}
