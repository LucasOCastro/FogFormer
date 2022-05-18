using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FogFormer
{
    [RequireComponent((typeof(Collider2D)))]
    public class GroundedController : MonoBehaviour
    {
        [SerializeField] private LayerMask mask;
        [SerializeField] private float castDistance = .1f;
        [SerializeField] private float sideCastOffset = .1f;
        
        public bool IsGrounded { get; private set; }
        public Vector2 GroundNormal { get; private set; }
        public Vector2 GroundSlope { get; private set; }
        
        private Collider2D _collider;
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        
        private void Update()
        {
            UpdateGroundedStatus();
#if UNITY_EDITOR
            foreach (var o in GetCastOrigins())
            {
                Debug.DrawRay( o, Vector2.down * castDistance, IsGrounded ? Color.green : Color.red);
            }
            //Debug.DrawRay(_collider.bounds.CenterBottom(), GroundSlope * 2.5f, Color.yellow);
#endif
        }

        private IEnumerable<Vector2> GetCastOrigins()
        {
            var bounds = _collider.bounds;
            float bottom = bounds.min.y;
            float centerX = bounds.center.x;
            yield return new Vector2(centerX, bottom);
            yield return new Vector2(centerX - sideCastOffset, bottom);
            yield return new Vector2(centerX + sideCastOffset, bottom);
        }

        private void UpdateGroundedStatus()
        {
            foreach (var o in GetCastOrigins())
            {
                var cast = Physics2D.Raycast(o, Vector2.down, castDistance, mask);
                if (cast)
                {
                    IsGrounded = true;
                    GroundNormal = cast.normal;
                    GroundSlope = Vector2.Perpendicular(GroundNormal);
                    return;
                }
            }
            IsGrounded = false;
            GroundNormal = Vector2.zero;
            GroundSlope = Vector2.zero;
        }
    }
}