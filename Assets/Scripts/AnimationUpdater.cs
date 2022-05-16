using System;
using UnityEngine;

namespace FogFormer
{
    public class AnimationUpdater : MonoBehaviour
    {
        private static readonly int Hanging = Animator.StringToHash("hanging");
        private static readonly int Slash = Animator.StringToHash("slash");
        
        [SerializeField] private PlayerController controller;
        [SerializeField] private WallGrabber wallGrabber;

        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void UpdateWallGrab(bool grabbing)
        {
            _animator.SetBool(Hanging, grabbing);
        }

        private void OnEnable()
        {
            wallGrabber.OnWallGrabUpdate += UpdateWallGrab;
        }

        private void OnDisable()
        {
            wallGrabber.OnWallGrabUpdate -= UpdateWallGrab;
        }
    }
}