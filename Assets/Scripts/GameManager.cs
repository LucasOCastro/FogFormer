using UnityEngine;
using UnityEngine.SceneManagement;

namespace FogFormer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private RectTransform levelRestartUI;
        [SerializeField] private int gameSceneIndex;
        [SerializeField] private int endSceneIndex;

        private void Awake()
        {
            HealthManager health = player.GetComponent<HealthManager>();
            health.OnDeath += OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            levelRestartUI.gameObject.SetActive(true);
        }

        public void RestartLevel()
        {
            levelRestartUI.gameObject.SetActive(false);
            SceneManager.LoadScene(gameSceneIndex);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void EndLevel()
        {
            SceneManager.LoadScene(endSceneIndex);
        }
    }
}