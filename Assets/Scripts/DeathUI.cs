using System;
using UnityEngine;
using UnityEngine.UI;

namespace FogFormer
{
    public class DeathUI : MonoBehaviour
    {
        [SerializeField] private Button retryButton;
        [SerializeField] private Button quitButton;
        private void Awake()
        {
            retryButton.onClick.AddListener(GameManager.RestartScene);
            quitButton.onClick.AddListener(GameManager.QuitGame);
        }
    }
}