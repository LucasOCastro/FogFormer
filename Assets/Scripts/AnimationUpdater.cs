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
        
        private PlayerDasher _dasher;
        private WallGrabber _wallGrabber;
        private HealthManager _health;
        private PlayerAttacker _attacker;
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _dasher = GetComponent<PlayerDasher>();
            _wallGrabber = GetComponent<WallGrabber>();
            _health = GetComponent<HealthManager>();
            _attacker = GetComponent<PlayerAttacker>();
        }

        private void OnDamage() => _animator.SetTrigger(Hurt);
        private void OnAttack() => _animator.SetTrigger(Slash);

        private void UpdateWallGrab(bool grabbing) => _animator.SetBool(Hanging, grabbing);
        private void TriggerDash() => _animator.SetTrigger(Dash);

        private void OnEnable()
        {
            _wallGrabber.OnWallGrabUpdate += UpdateWallGrab;
            _dasher.OnDash += TriggerDash;
            _health.OnDamage += OnDamage;
            _attacker.OnAttack += OnAttack;
        }

        

        private void OnDisable()
        {
            _wallGrabber.OnWallGrabUpdate -= UpdateWallGrab;
            _dasher.OnDash -= TriggerDash;
            _health.OnDamage -= OnDamage;
            _attacker.OnAttack -= OnAttack;
        }
    }
}