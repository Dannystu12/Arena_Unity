using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    [SerializeField] GameObject uiPrefab;
    [SerializeField] Transform target;
    [SerializeField] float activeTime = 5f;
    [SerializeField] float lastVisibleTime;

    Transform ui;
    Image healthSlider;
    Transform cam;

	// Use this for initialization
	void Start () {

        cam = Camera.main.transform;

		foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if(c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                break;
            }
        }
        GetComponent<Character>().OnHealthChanged +=  OnHealthChanged;
        ui.gameObject.SetActive(false);
    }
	

	void LateUpdate () {
        ui.position = target.position;
        ui.forward = -cam.forward;
        if(Time.time - lastVisibleTime > activeTime)
        {
            ui.gameObject.SetActive(false);
        }
	}

    void OnHealthChanged(int maxHp, int currentHp)
    {
        ui.gameObject.SetActive(true);
        lastVisibleTime = Time.time;
        float healthPct = (float)currentHp / maxHp;
        healthSlider.fillAmount = healthPct;
    }
}
