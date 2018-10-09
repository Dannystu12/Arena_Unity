
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{

    [SerializeField] float locomotionSmoothTime = 0.1f;
    //private bool locked = false;

    private Animator animator;
    private NavMeshAgent agent;

    //PlayerController playerController;

    // use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionSmoothTime, Time.deltaTime);
    }

    //private void OnAttack()
    //{
    //    animator.SetTrigger("attack");
    //}

    //public bool IsLocked()
    //{
    //    return locked;
    //}

    //public void Lock()
    //{
    //    locked = true;
    //}

    //public void Unlock()
    //{
    //    locked = false;
    //}
}
