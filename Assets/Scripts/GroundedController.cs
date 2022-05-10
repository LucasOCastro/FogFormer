using System;
using System.Linq;
using UnityEngine;

namespace FogFormer
{
    [RequireComponent((typeof(Rigidbody2D)))]
    public class GroundedController : MonoBehaviour
    {
        [SerializeField] private LayerMask mask;
        [SerializeField] private float castDistance = .1f;
        [SerializeField] private float sideCastOffset = .1f;

        private Collider2D _collider;
        private Vector2[] _castOffsets;
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            var bounds = _collider.bounds;
            float bottom = bounds.min.y;
            Vector2 center = new Vector2(bounds.center.x, bottom);
            Vector2 min = new Vector2(center.x - sideCastOffset, bottom);
            Vector2 max = new Vector2(center.x + sideCastOffset, bottom);
            _castOffsets = new[] {min, center, max};
        }

        #if UNITY_EDITOR
        private void Update()
        {
            foreach (var o in _castOffsets)
            {
                Vector2 center = _collider.bounds.center;
                Debug.DrawRay( center + o, Vector2.down * castDistance, Color.blue);
            }
        }
        #endif

        //Is linq a good idea? Rider recommended it but idk if i like it
        public bool IsGrounded()
        {
            Vector2 center = _collider.bounds.center;
            return _castOffsets.Any(o => Physics2D.Raycast(center + o, -transform.up, castDistance, mask));
        }
    }
}