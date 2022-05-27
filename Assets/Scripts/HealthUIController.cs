using System;
using UnityEngine;
using System.Collections.Generic;

namespace FogFormer
{
    public class HealthUIController : MonoBehaviour
    {
        [SerializeField] private HealthManager healthManager;
        [SerializeField] private GameObject healthIconPrefab;

        private int _activeCount;
        private List<GameObject> _instantiatedIcons = new List<GameObject>();
        private void Awake()
        {
            for (int i = 0; i < healthManager.MaxHealth; i++)
            {
                GameObject icon = Instantiate(healthIconPrefab, this.transform);
                _instantiatedIcons.Add(icon);
            }
            _activeCount = healthManager.MaxHealth;
        }
        
        private void OnDamage()
        {
            for (int i = healthManager.Health; i < _activeCount; i++)
            {
                _instantiatedIcons[i].SetActive(false);
            }
            _activeCount = healthManager.Health;
        }
        
        private void OnEnable()
        {
            healthManager.OnDamage += OnDamage;
        }
        private void OnDisable()
        {
            healthManager.OnDamage -= OnDamage;
        }
    }
}