using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour {

    [SerializeField] float stoppingDistanceFactor = 0.9f;
    [SerializeField] float lookSpeed = 5f;

    private NavMeshAgent agent;
    private Transform target;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
    void Update()
    {
        if(target != null)
        {
            MoveToPoint(target.position);
            FaceTarget();
        }
    }


	public void MoveToPoint(Vector3 point)
    {
        agent.isStopped = false;
        agent.SetDestination(point);
    }

    public void StopMoving()
    {
        agent.isStopped = true;
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.GetRadius() * stoppingDistanceFactor;
        agent.updateRotation = false;
        target = newTarget.GetInteractionTransform();
    }

    public void  StopFollowingTarget()
    {
        agent.updateRotation = true;
        agent.stoppingDistance = 0;
        target = null;
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
    }
}
