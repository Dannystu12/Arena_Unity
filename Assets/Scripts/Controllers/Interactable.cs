
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] float radius = 35f;
    [SerializeField] Transform interactionTransform;
    [SerializeField] GameObject targetIndicatorProjector;

    private bool isFocus = false;
    private bool hasInteracted = false;
    private Transform player;
    private Projector focusIndicator;

    private void Start()
    {
        targetIndicatorProjector.SetActive(false);
    }



    private void Update()
    {
        if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }


    //Show interaction range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public float GetRadius()
    {
        return radius;
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
        targetIndicatorProjector.SetActive(true);
    }

    public void OnDefocused()
    {
        hasInteracted = false;
        isFocus = false;
        player = null;
        targetIndicatorProjector.SetActive(false);
    }

    public virtual void Interact()
    {
    }

    public Transform GetInteractionTransform()
    {
        return interactionTransform;
    }
}
