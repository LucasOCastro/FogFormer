using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace FogFormer
{
    public class StaminaUIController : MonoBehaviour
    {
        [SerializeField] private PlayerDasher dasher;

        private Image _image;
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            _image.fillAmount = dasher.DashCooldownProgress;
        }
    }
}