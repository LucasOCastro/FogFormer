using UnityEngine;

namespace FogFormer
{
    public class GameEndZone : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        //TODO temp
        private void OnTriggerEnter2D(Collider2D col)
        {
            gameManager.EndLevel();
        }
    }
}