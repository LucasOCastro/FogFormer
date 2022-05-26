using System;
using UnityEngine;

namespace FogFormer
{
    public class AnimationUpdater : MonoBehaviour
    {
        private static readonly int Hanging = Animator.StringToHash("hanging");
        private static readonly int Slash = Animator.StringToHash("slash");
        private static readonly int Dash = Animator.StringToHash("dash");
        private static readonly int Hurt = Animator.StringToHash("hurt");
        
        [SerializeField] private PlayerDasher dasher;
        [SerializeField] private WallGrabber wallGrabber;
        [SerializeField] private HealthManager health;

        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnDamage(int before, int after) => _animator.SetTrigger(Hurt);

        private void UpdateWallGrab(bool grabbing) => _animator.SetBool(Hanging, grabbing);
        private void TriggerDash() => _animator.SetTrigger(Dash);

        private void OnEnable()
        {
            wallGrabber.OnWallGrabUpdate += UpdateWallGrab;
            dasher.OnDash += TriggerDash;
            health.OnDamage += OnDamage;
        }
        private void OnDisable()
        {
            wallGrabber.OnWallGrabUpdate -= UpdateWallGrab;
            dasher.OnDash -= TriggerDash;
            health.OnDamage -= OnDamage;
        }
    }
}