using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    [Header("Health Bar")]
    [SerializeField] GameObject uiPrefab;
    [SerializeField] Transform target;
    [SerializeField] float activeTime = 5f;
    [SerializeField] float lastVisibleTime;

    [Header("Combat Text Popup")]
    [SerializeField] float removeDelay = 5f;
    [SerializeField] GameObject combatTextPrefab;
    [SerializeField] float damageTextDuration = 2f;
    [SerializeField] Color missColor;
    [SerializeField] Color hitColor;
    [SerializeField] Color critColor;
    [SerializeField] Vector3 offset = new Vector3(0, 100f, 0);

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
                Vector3 adjustedPosition = target.position + offset;
                damageText.transform.position = new Vector3(adjustedPosition.x, damageText.transform.position.y, adjustedPosition.z);
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
        
        Text text = damageText.GetComponent<Text>();
        text.text = damage.ToString();

        if (damage == 0)
        {
            text.color = missColor;
        }
        else if(crit)
        {
            text.color = critColor;
            text.fontSize += text.fontSize;
        }
        else
        {
            text.color = hitColor;
        }

        Destroy(damageText, damageTextDuration);
    }

    void OnDeath()
    {
        Destroy(ui.gameObject);

    }
}
