using System;
using UnityEngine;

namespace FogFormer
{
    //This is honestly more of a PlayerFreezer or something like that
    public class StatusManager : MonoBehaviour
    {
        private bool _stunned;
        private float _stunSeconds, _stunTimer;
        private IStunnable[] _stunnables;
        private void Awake()
        {
            _stunnables = GetComponents<IStunnable>();
        }

        public void PermaStun() => Stun(-1);
        public void Stun(float seconds)
        {
            _stunned = true;
            _stunSeconds = seconds;
            _stunTimer = 0;

            foreach (var stunnable in _stunnables)
            {
                stunnable.IsStunned = true;
            }
        }

        public void DeStun()
        {
            if (!_stunned) return;
            
            _stunned = false;
            foreach (var stunnable in _stunnables)
            {
                stunnable.IsStunned = false;
            }
        }

        private void Update()
        {
            if (!_stunned) return;
            _stunTimer += Time.deltaTime;
            if (_stunSeconds >= 0 && _stunTimer > _stunSeconds)
            {
                DeStun();
            }
        }
    }
}