using UnityEngine;

namespace FogFormer
{
    //This is honestly more of a PlayerFreezer or something like that
    public class StatusManager : MonoBehaviour
    {
        //TEMP VERY TEMP
        [SerializeField] private MonoBehaviour[] stunComponents;

        private bool _stunned;
        private float _stunSeconds, _stunTimer;

        public void PermaStun() => Stun(-1);
        public void Stun(float seconds)
        {
            _stunned = true;
            _stunSeconds = seconds;
            _stunTimer = 0;

            foreach (var comp in stunComponents)
            {
                comp.enabled = false;
            }
        }

        public void DeStun()
        {
            if (!_stunned) return;
            
            _stunned = false;
            foreach (var comp in stunComponents)
            {
                comp.enabled = true;
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