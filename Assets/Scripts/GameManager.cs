using UnityEngine;
using UnityEngine.SceneManagement;

namespace FogFormer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private RectTransform levelRestartUIPrefab;
        [SerializeField] private int gameSceneIndex;
        [SerializeField] private int endSceneIndex;
        [SerializeField] private int tutorialSceneIndex;
        
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            HealthManager health = player.GetComponent<HealthManager>();
            health.OnDeath += OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            Instantiate(levelRestartUIPrefab);
        }

        public static void LoadTutorial() => SceneManager.LoadScene(Instance.tutorialSceneIndex);
        public static void LoadMainLevel() => SceneManager.LoadScene(Instance.gameSceneIndex);
        public static void EndLevel() => SceneManager.LoadScene(Instance.endSceneIndex);
        public static void QuitGame() => Application.Quit();

        
    }
}