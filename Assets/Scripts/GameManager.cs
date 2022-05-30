using UnityEngine;
using UnityEngine.SceneManagement;

namespace FogFormer
{
    public class GameManager : MonoBehaviour
    {
        //A lot of unpleasant practices here :(
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private int tutorialSceneIndex = 0;
        [SerializeField] private int gameSceneIndex = 1;
        [SerializeField] private int endSceneIndex = 2;
        
        
        public static GameManager Instance { get; private set; }

        private GameObject _player;

        public GameObject Player
        {
            get
            {
                if (_player == null)
                {
                    _player = GameObject.FindWithTag(playerTag);
                }
                if (_player != null)
                {
                    var health = _player.GetComponent<HealthManager>();
                    health.OnDeath += OnPlayerDeath;
                }
                return _player;
            } 
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        private void OnPlayerDeath()
        {
            var deathUI = FindObjectOfType<DeathUI>(true);
            deathUI.gameObject.SetActive(true);
        }

        public static void RestartScene() => Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
        public static void LoadTutorial() => Instance.LoadScene(Instance.tutorialSceneIndex);
        public static void LoadMainLevel() => Instance.LoadScene(Instance.gameSceneIndex);
        public static void EndLevel() => Instance.LoadScene(Instance.endSceneIndex);
        public static void QuitGame() => Application.Quit();

        
    }
}