using UnityEngine;

namespace FogFormer
{
    public class DirectionManager : MonoBehaviour
    {
        [SerializeField] private Mover mover;

        public int Direction => _lastDirection;
        
        private int _lastDirection = 1;
        private void Update()
        {
            if (mover != null && mover.IsMoving)
            {
                SetFlip(mover.MoveDirection);
            }
        }
        
        public void SetFlip(int sign)
        {
            if (sign != _lastDirection)
            {
                Vector2 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                _lastDirection = sign;
            }
        }

        public void Face(Vector2 pos)
        {
            float dif = pos.x - transform.position.x;
            int sign = dif < 0 ? -1 : 1;
            SetFlip(sign);
        }
    }
}