using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour {

    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI damageText;

    private void Start()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
    }

    public void SetText(string text)
    {
        damageText.SetText(text);
    }

    public void SetColor(Color c)
    {
        damageText.faceColor = c;
    }

} 
