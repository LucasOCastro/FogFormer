using System;
using UnityEngine;

namespace FogFormer
{
    //This is horrible
    [RequireComponent(typeof(PlayerController), typeof(GroundedController))]
    public class WallGrabber : MonoBehaviour
    {
        [SerializeField] private float grabDistance;
        [SerializeField] private LayerMask mask;
        [SerializeField] private float releaseDisplacement;
        [SerializeField] private float climbCastHeight;
        [SerializeField] private bool onlyGrabOntoLedge;
        
        [Header("Wall Jump")]
        [SerializeField] private float wallJumpForce;
        [Range(0, 90)]
        [SerializeField] private float wallJumpAngle;
        [SerializeField] private string wallJumpButton;

        [Header("Input")] 
        [SerializeField] private string releaseButton;
        [SerializeField] private string climbButton;

        public Action<bool> OnWallGrabUpdate;
        
        private int _grabSign;
        public bool IsGrabbing => _grabSign != 0;
        
        //Not a big fan of this currently
        private bool _grabbedLedge;
        private Vector2 _edgeFloorPoint;

        private Rigidbody2D _rb;
        private Collider2D _collider;
        private PlayerController _controller;
        private GroundedController _grounded;
        private StatusManager _statusManager;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _controller = GetComponent<PlayerController>();
            _grounded = GetComponent<GroundedController>();
            _statusManager = GetComponent<StatusManager>();
        }

        private void SetGrab(int grabSign)
        {
            if (_grabSign == grabSign) return;
            
            if (grabSign == 0) {
                _statusManager.DeStun();
                _rb.constraints ^= (RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX);
                _rb.position += Vector2.right * -_grabSign * releaseDisplacement;
            }
            else {
                _statusManager.PermaStun();
                _rb.constraints |= (RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX);
            }
            _grabSign = grabSign;
            OnWallGrabUpdate?.Invoke(grabSign != 0);
        }

        private void TryGrab()
        {
            int xSign = _controller.InputDirection;
            if (xSign == 0) return;
            
            Vector2 origin = (xSign < 0) ? _collider.bounds.CenterLeft() : _collider.bounds.CenterRight();
            Vector2 ray = Vector2.right * xSign;
            if (Physics2D.Raycast(origin, ray, grabDistance, mask))
            {
                //this ugly
                _grabbedLedge = WallGrabUtility.DetectLedge(_collider, climbCastHeight, xSign, mask, out _edgeFloorPoint);
                if (!onlyGrabOntoLedge || _grabbedLedge)
                {
                    SetGrab(xSign);
                    _rb.position = new Vector2(_rb.position.x,_edgeFloorPoint.y - _collider.bounds.extents.y);
                }
            }
        }

        //TEMP
        //Needs refactoring
        private void TryClimb()
        {
            if (!_grabbedLedge)
            {
                return;
            }
            SetGrab(0);
            Vector2 finalPosition = _edgeFloorPoint + Vector2.up * _collider.bounds.extents.y;
            _rb.position = finalPosition;
        }

        private void WallJump()
        {
            
            float radAngle = Mathf.Deg2Rad * wallJumpAngle;
            Vector2 direction = new Vector2(Mathf.Cos(radAngle) * -_grabSign, Mathf.Sin(radAngle));
            _rb.AddForce(direction * wallJumpForce, ForceMode2D.Impulse);
            SetGrab(0);
            
        }
        
        private void Update()
        {
            if (!IsGrabbing)
            {
                return;
            }
            
            if (wallJumpButton != "" && Input.GetButtonDown(wallJumpButton))
            {
                WallJump();
            }
            else if (Input.GetButtonDown(releaseButton))
            {
                SetGrab(0);
            }
            else if (Input.GetButtonDown(climbButton))
            {
                TryClimb();
            }
        }
        private void FixedUpdate()
        {
            if (_grounded.IsGrounded)
            {
                return;
            }
            
            if (!IsGrabbing)
            {
                TryGrab();
            }
        }
    }
}