
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
    [SerializeField] AudioClip[] hitSound;
    [SerializeField] AudioClip[] missSound;
    [SerializeField] AudioClip[] deathSound;
    [Tooltip("One in:")][SerializeField] int chanceForBlockSound = 5;
    

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
        //float speedPercent = agent.velocity.magnitude / agent.speed;
        float speedPercent = agent.isStopped ? 0 : 1;
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
        audioSource.PlayOneShot(GetRandomSound(deathSound));
    }

    void OnHealthChanged(int maxHp, int currentHp, int damage, bool crit)
    {
        if(damage > 0)
        {
            audioSource.PlayOneShot(GetRandomSound(hitSound), 0.7f);
            bloodVfx.Play();
        }
        else
        {
            if(Roll(chanceForBlockSound))
                audioSource.PlayOneShot(GetRandomSound(missSound), 0.5f);
        }
    }

    AudioClip GetRandomSound(AudioClip[] clips)
    {
        
        int i = Random.Range(0, clips.Length);
        return clips[i];
    }

    bool Roll(int chance)
    {
        int i = Random.Range(1, chance + 1);
        return i == chance;
    }


}
