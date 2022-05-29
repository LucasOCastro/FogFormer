using UnityEngine;

namespace FogFormer
{
    public class CloseGate : Triggerable
    {
        private static readonly int Closed = Animator.StringToHash("closed");
        
        
        [SerializeField] private bool startClosed;
        public bool IsClosed { get; private set; }
        
        private Collider2D _collider;
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
            
            IsClosed = startClosed;
            PostChange();
        }

        private void PostChange()
        {
            _collider.enabled = IsClosed;
            _animator.SetBool(Closed, IsClosed);
        }

        public override void Trigger()
        {
            IsClosed = !IsClosed;
            PostChange();
        }
    }
}