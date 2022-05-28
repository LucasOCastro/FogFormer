using UnityEngine;

namespace FogFormer
{
    public class PlayerDasher : MonoBehaviour, IStunnable
    {
        public System.Action OnDash;

        [SerializeField] private float dashDistance;
        [SerializeField] private float longDashDistance;
        [SerializeField] private string dashButton;
        [SerializeField] LayerMask collisionMask;

        [SerializeField] private float cooldownSeconds;

        [SerializeField] private ParticleSystem dashParticlePrefab;
        [SerializeField] private AudioClip dashAudio;
        
        public bool IsStunned { get; set; }

        public float DashCooldownProgress => Mathf.Clamp01(_cooldownTimer / cooldownSeconds);

        private float _cooldownTimer;
            
        private PlayerController _controller;
        private Collider2D _collider;
        private Rigidbody2D _rb;
        private AudioSource _audio;
        private void Awake()
        {
            _controller = GetComponent<PlayerController>();
            _collider = GetComponent<Collider2D>();
            _rb = GetComponent<Rigidbody2D>();
            _audio = GetComponent<AudioSource>();
            _cooldownTimer = cooldownSeconds;
        }

        private Vector2 GetEndPos(int directionSign)
        {
            Vector2 direction = (directionSign < 0) ? Vector2.left : Vector2.right;
            Bounds bounds = _collider.bounds;
            Vector2 castOrigin = (Vector2)bounds.center + (direction * bounds.extents.x);
            float distance = _controller.MoveInput.x != 0 ? longDashDistance : dashDistance;
            var cast = Physics2D.Raycast(castOrigin, direction, distance, collisionMask);

            if (cast)
            {
                Vector2 endPos = cast.point - direction * bounds.extents.x;
                return Mathf.Approximately(endPos.x, castOrigin.x) ? bounds.center : endPos;
            }
            return castOrigin + direction * distance;
        }

        private void PlayDashEffects()
        {
            Instantiate(dashParticlePrefab, _rb.position, Quaternion.identity);
            _audio.PlayOneShot(dashAudio);
        }

        private void TryDash()
        {
            int lookDirection = _controller.LookDirection;
            Vector2 endPos = GetEndPos(lookDirection);

            if (endPos == _rb.position)
            {
                return;
            }
            
            _rb.MovePosition(GetEndPos(lookDirection));
            _cooldownTimer = 0;
            OnDash?.Invoke();
            PlayDashEffects();
        }

        private void Update()
        {
            _cooldownTimer += Time.deltaTime;
            if (!IsStunned && _cooldownTimer > cooldownSeconds && Input.GetButtonDown(dashButton))
            {
                TryDash();
            }
        }
    }
}