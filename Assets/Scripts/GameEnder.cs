using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FogFormer
{
    public class GameEnder : MonoBehaviour
    {
        [SerializeField] private string endSceneName;
        //TODO temp
        private void OnTriggerEnter2D(Collider2D col)
        {
            SceneManager.LoadScene(endSceneName);
        }
    }
}