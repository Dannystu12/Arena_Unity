
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
      
    [SerializeField] float locomotionSmoothTime = 0.1f;
    [SerializeField] AnimationClip[] attackAnimations;

    protected Animator animator;
    private NavMeshAgent agent;
    protected CharacterCombat combat;
    protected AnimatorOverrideController overrideController;

    // use this for initialization
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;
        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionSmoothTime, Time.deltaTime);
        animator.SetBool("inCombat", combat.InCombat);
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, attackAnimations.Length);
        overrideController["Attack"] = attackAnimations[attackIndex];
    }

}
