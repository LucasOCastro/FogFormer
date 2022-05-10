using System;
using UnityEngine;
using UnityEngine.Tilemaps;

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

        [Header("Release Settings")] 
        [SerializeField] private bool releaseFromMovingAway;
        [SerializeField] private bool climbByMovingIntoWall;
        [SerializeField] private string releaseButton;
        
        private int _grabSign;
        public bool IsGrabbing => _grabSign != 0;
        
        //Not a big fan of this currently
        private bool _grabbedLedge;
        private Vector2 _edgeFloorPoint;

        private Rigidbody2D _rb;
        private Collider2D _collider;
        private PlayerController _controller;
        private GroundedController _grounded;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _controller = GetComponent<PlayerController>();
            _grounded = GetComponent<GroundedController>();
        }

        private void SetGrab(int grabSign)
        {
            _grabSign = grabSign;
            if (grabSign == 0) {
                _rb.constraints ^= (RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX);
                _rb.position += Vector2.right * _grabSign * releaseDisplacement; 
            }
            else {
                _rb.constraints |= (RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX);
            }
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
            Vector2 finalPosition = _edgeFloorPoint + Vector2.up * _collider.bounds.extents.y;
            transform.position = finalPosition;
            SetGrab(0);
        }

        private void WallJump()
        {
            Quaternion rotation = Quaternion.AngleAxis(wallJumpAngle, (_grabSign == -1) ? Vector3.forward : Vector3.back);
            Vector2 direction = rotation * (Vector2.right * -_grabSign);
            //Quaternion.AngleAxis(wallJumpAngle, Vector3.forward);
            SetGrab(0);
            _rb.AddForce(direction * wallJumpForce, ForceMode2D.Impulse);
        }
        
        private void Update()
        {
            //TODO remove
            #if UNITY_EDITOR
            if (_grabSign != 0)
            {
                Debug.DrawRay(_collider.bounds.CenterTop() + (Vector2.up * climbCastHeight), Vector2.right * _grabSign * _collider.bounds.size.x * 1.5f, Color.red);
                Debug.DrawRay(_collider.bounds.CenterTop() + (Vector2.up * climbCastHeight) + Vector2.right * _grabSign * _collider.bounds.size.x, Vector2.down *
                    (climbCastHeight + _collider.bounds.extents.y), Color.red);    
            }
            #endif
            
            if (_grounded.IsGrounded())
            {
                return;
            }
            
            if (!IsGrabbing)
            {
                TryGrab();
                return;
            }
            
            float yInput = _controller.MoveInput.y;
            if (wallJumpButton != "" && Input.GetButtonDown(wallJumpButton))
            {
                WallJump();
            }
            else if (yInput < 0 || (releaseFromMovingAway && _controller.InputDirection == -_grabSign) || (releaseButton != "" && Input.GetButtonDown(releaseButton)))
            {
                SetGrab(0);
            }
            else if (yInput > 0 || (climbByMovingIntoWall && _controller.InputDirection == _grabSign))
            {
                TryClimb();
            }
        }
    }
}