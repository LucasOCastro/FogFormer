using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.position = destination.position;
    }
}
