using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour {

    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
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
}
