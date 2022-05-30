using System;
using UnityEngine;
using UnityEngine.UI;

namespace FogFormer
{
    public class EndMenu : MonoBehaviour
    {
        [SerializeField] private Button tutorialButton;
        [SerializeField] private Button levelButton;
        [SerializeField] private Button quitButton;

        private void OnEnable()
        {
            tutorialButton.onClick.AddListener(GameManager.LoadTutorial);
            levelButton.onClick.AddListener(GameManager.LoadMainLevel);
            quitButton.onClick.AddListener(GameManager.QuitGame);
        }

        private void OnDisable()
        {
            tutorialButton.onClick.RemoveAllListeners();
            levelButton.onClick.RemoveAllListeners();
            quitButton.onClick.RemoveAllListeners();
        }
    }
}