using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour {

    [Header("Health Bar")]
    [SerializeField] GameObject uiPrefab;
    [SerializeField] Transform target;
    [SerializeField] float activeTime = 5f;
    [SerializeField] float lastVisibleTime;
    [SerializeField] float removeDelay = 5f;

    [Header("Combat Text Popup")]
    [SerializeField] GameObject combatTextPrefab;
    [SerializeField] Vector3 damageOffset = new Vector3(0f, -250f, 0f);


    Transform ui;
    Image healthSlider;
    Transform cam;
    Canvas canvas;
    GameObject damageText;

	// Use this for initialization
	void Start () {

        cam = Camera.main.transform;

		foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if(c.renderMode == RenderMode.WorldSpace)
            {
                canvas = c;
                ui = Instantiate(uiPrefab, canvas.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                break;
            }
        }
        
        GetComponent<Character>().OnHealthChanged +=  OnHealthChanged;
        ui.gameObject.SetActive(false);

        GetComponent<Character>().OnDeath += OnDeath;
    }
	

	void LateUpdate () { 

        if(damageText != null)
        {
            RectTransform tempRect = damageText.GetComponent<RectTransform>();
            if(tempRect != null)
            {

                damageText.transform.position = new Vector3(target.position.x + damageOffset.x, damageText.transform.position.y + damageOffset.y, target.position.z + damageOffset.z);
                tempRect.forward = cam.forward;
            }

        } 
        if(ui != null)
        { 
            ui.position = target.position;
            ui.forward = -cam.forward;
            if (Time.time - lastVisibleTime > activeTime)
            {
                ui.gameObject.SetActive(false);
            }
        }

	}

    void OnHealthChanged(int maxHp, int currentHp, int damage, bool crit)
    {
        ui.gameObject.SetActive(true);
        lastVisibleTime = Time.time;
        float healthPct = (float)currentHp / maxHp;
        healthSlider.fillAmount = healthPct;
        if(healthPct == 0)
        {
            Destroy(ui.gameObject, removeDelay);
        }
        CreateDamageText(damage, crit);
    }

    void CreateDamageText(int damage, bool crit)
    {
       
        if (damageText != null) Destroy(damageText);
        damageText = Instantiate(combatTextPrefab, canvas.transform);
        RectTransform tempRect = damageText.GetComponent<RectTransform>();
        tempRect.transform.position = target.position;
        damageText.GetComponent<FloatingText>().SetDamage(damage, crit);
    } 
     
    void OnDeath()
    {
        Destroy(ui.gameObject);
    }
}
