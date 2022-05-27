using UnityEngine;

namespace FogFormer
{
    public abstract class Mover : MonoBehaviour, IStunnable
    {
        public bool IsStunned { get; set; }
        
        public abstract bool IsMoving { get; }
        public abstract int MoveDirection { get; }
    }
}