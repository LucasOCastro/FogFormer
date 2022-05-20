using UnityEngine;

namespace FogFormer
{
    public class PlayerDasher : MonoBehaviour
    {
        [SerializeField] private float dashDistance;
        [SerializeField] private string dashButton;
        [SerializeField] LayerMask collisionMask;

        [SerializeField] private float cooldownSeconds;

        private float _cooldownTimer;
            
        private PlayerController _controller;
        private Collider2D _collider;
        private Rigidbody2D _rb;
        private void Awake()
        {
            _controller = GetComponent<PlayerController>();
            _collider = GetComponent<Collider2D>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private Vector2 GetEndPos(int directionSign)
        {
            Vector2 direction = (directionSign < 0) ? Vector2.left : Vector2.right;
            Bounds bounds = _collider.bounds;
            Vector2 castOrigin = (Vector2)bounds.center + (direction * bounds.extents.x);
            var cast = Physics2D.Raycast(castOrigin, direction, dashDistance, collisionMask);

            if (!cast)
            {
                return castOrigin + direction * dashDistance;
            }
            return cast.point - direction * bounds.extents.x;
        }

        void Dash()
        {
            int lookDirection = _controller.LookDirection;
            _rb.MovePosition(GetEndPos(lookDirection));
            _cooldownTimer = cooldownSeconds;
        }

        private void Update()
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0 && Input.GetButtonDown(dashButton))
            {
                Dash();
            }
        }
    }
}