using UnityEngine;

namespace FogFormer
{
    public class GameEndZone : MonoBehaviour
    {
        private enum EndType { Tutorial, FinalLevel }
        [SerializeField] private EndType type;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            switch (type)
            {
                case EndType.FinalLevel:
                    GameManager.EndLevel();
                    break;
                case EndType.Tutorial:
                    GameManager.LoadMainLevel();
                    break;
            }
        }
    }
}