
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    [SerializeField] float lookRadius = 100f;
    [SerializeField] float rotationSpeed = 5f;

    Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        target = GameSession.instance.GetPlayer().transform;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {
                FaceTarget();
                //Attack
                
            }
        }
	}

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    //Draw look radius in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
