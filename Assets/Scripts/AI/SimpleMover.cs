using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace FogFormer.AI.Nodes
{
    public class SimpleMover : Mover
    {
        [SerializeField] private float speed;
        
        protected override void MoveToTarget()
        {
            transform.position = Vector2.MoveTowards(transform.position, Target, speed * Time.deltaTime);
        }
    }
}