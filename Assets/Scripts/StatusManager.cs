using UnityEngine;

namespace FogFormer
{
    public class StatusManager : MonoBehaviour
    {
        //TEMP VERY TEMP
        [SerializeField] private MonoBehaviour[] stunComponents;

        private bool _stunned;
        private float _stunSeconds, _stunTimer;
        
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

        private void DeStun()
        {
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
            if (_stunTimer > _stunSeconds)
            {
                DeStun();
            }
        }
    }
}