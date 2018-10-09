
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] float radius = 35f;

    //Show interaction range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public float GetRadius()
    {
        return radius;
    }
}
