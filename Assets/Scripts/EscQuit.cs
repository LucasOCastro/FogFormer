using System;
using UnityEngine;
using UnityEngine.UI;

namespace FogFormer
{
    public class EscQuit : MonoBehaviour
    {
        [SerializeField] private GameObject escMenu;
        [SerializeField] private Button quitButton, restartButton;

        private void OnEnable()
        {
            quitButton.onClick.AddListener(GameManager.QuitGame);
            restartButton.onClick.AddListener(GameManager.RestartScene);
        }

        private void OnDisable()
        {
            quitButton.onClick.RemoveAllListeners();
            restartButton.onClick.RemoveAllListeners();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Toggle();
            }
        }

        private void Toggle()
        {
            escMenu.SetActive(!escMenu.activeSelf);
        }
    }
}