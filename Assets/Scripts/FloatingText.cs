using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour {



    [Header("Colors")]
    [SerializeField] Color missColor;
    [SerializeField] Color hitColor;
    [SerializeField] Color critColor;

    Animator animator;
    TextMeshProUGUI damageText;

    void Awake()
    {
        animator = GetComponent<Animator>();
        damageText = GetComponent<TextMeshProUGUI>();
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
    }

    public void SetDamage(int damage, bool crit)
    {
        damageText.SetText(damage.ToString());
        if (crit) 
        {
            damageText.faceColor = critColor;
            damageText.fontSize += damageText.fontSize;
        }
        else if(damage == 0)
        {
            damageText.faceColor = missColor;
        }
        else
        {
            damageText.faceColor = hitColor;
        }

    }

    public void SetColor(Color c)
    {
        damageText.faceColor = c;
    }

} 
