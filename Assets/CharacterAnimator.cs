using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour {

    [SerializeField] float locomotionAnimationSmoothTime = 0.1f;

    Animator animator;
    PlayerController playerController;

	// Use this for initialization
	void Start () {
        animator =  GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        float speedPercent = playerController.IsMoving() ? 100 : 0; 
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
	}
}
