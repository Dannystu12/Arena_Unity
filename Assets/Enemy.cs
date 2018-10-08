using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{

    [SerializeField] float radius = 3f;

    bool isFocus = false;   
    Transform player;

    bool hasInteracted = false;

    void Update()
    {
        if (isFocus)   
        {
            float distance = Vector3.Distance(player.position, transform.position);

            if (!hasInteracted && distance <= radius)
            {

                hasInteracted = true;
                Interact();
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        hasInteracted = false;
        player = playerTransform;
    }


    public void OnDefocused()
    {
        isFocus = false;
        hasInteracted = false;
        player = null;
    }

    public virtual void Interact()
    {
        //Attack
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}