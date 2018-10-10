
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
      
    [SerializeField] float locomotionSmoothTime = 0.1f;
    [SerializeField] AnimationClip[] attackAnimations;

    protected Animator animator;
    private NavMeshAgent agent;
    protected CharacterCombat combat;
    [SerializeField] AnimatorOverrideController overrideController;

    [SerializeField] ParticleSystem bloodVfx;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip missSound;
    [SerializeField] AudioClip deathSound;
    

    // use this for initialization
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        if(overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        
        animator.runtimeAnimatorController = overrideController;
        combat.OnAttack += OnAttack;

        Character character = GetComponent<Character>();
        character.OnDeath += OnDeath;
        GetComponent<Character>().OnHealthChanged += OnHealthChanged;
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

    public void OnDeath()
    {
        //Death animation
        audioSource.PlayOneShot(deathSound);
    }

    void OnHealthChanged(int maxHp, int currentHp, int damage, bool crit)
    {
        if(damage > 0)
        {
            audioSource.PlayOneShot(hitSound);
            bloodVfx.Play();
        }
        else
        {
            audioSource.PlayOneShot(missSound);
        }
    }

}
