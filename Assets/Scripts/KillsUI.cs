using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KillsUI : MonoBehaviour {

    TextMeshProUGUI killText;
	// Use this for initialization
	void Start () {
        killText = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        killText.text = GameSession.instance.GetKills().ToString("000");
	}
}
