using UnityEngine;
using UnityEngine.SceneManagement;

namespace FogFormer
{
    public class GameManager : MonoBehaviour
    {
        //A lot of unpleasant practices here :(
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private int gameSceneIndex;
        [SerializeField] private int endSceneIndex;
        [SerializeField] private int tutorialSceneIndex;
        
        public static GameManager Instance { get; private set; }

        private GameObject _player;
        private DeathUI _deathUI;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            _player = GameObject.FindWithTag(playerTag);
            if (_player != null)
            {
                _deathUI = FindObjectOfType<DeathUI>(true);
                HealthManager health = _player.GetComponent<HealthManager>();
                health.OnDeath += OnPlayerDeath;
            }
            else _deathUI = null;
        }

        private void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
            
            _player = GameObject.FindWithTag(playerTag);
            if (_player != null)
            {
                _deathUI = FindObjectOfType<DeathUI>(true);
                HealthManager health = _player.GetComponent<HealthManager>();
                health.OnDeath += OnPlayerDeath;
            }
            else _deathUI = null;
        }

        private void OnPlayerDeath()
        {
            _deathUI.gameObject.SetActive(true);
        }

        public static void RestartScene() => Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
        public static void LoadTutorial() => Instance.LoadScene(Instance.tutorialSceneIndex);
        public static void LoadMainLevel() => Instance.LoadScene(Instance.gameSceneIndex);
        public static void EndLevel() => Instance.LoadScene(Instance.endSceneIndex);
        public static void QuitGame() => Application.Quit();

        
    }
}