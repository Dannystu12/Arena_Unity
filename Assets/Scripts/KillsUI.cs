using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillsUI : MonoBehaviour {

    Text killText;
	// Use this for initialization
	void Start () {
        killText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        killText.text = GameSession.instance.GetKills().ToString("000");
	}
}
