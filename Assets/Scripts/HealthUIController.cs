using System;
using UnityEngine;
using System.Collections.Generic;

namespace FogFormer
{
    public class HealthUIController : MonoBehaviour
    {
        [SerializeField] private HealthManager healthManager;
        [SerializeField] private GameObject healthIconPrefab;

        private List<GameObject> _instantiatedIcons = new List<GameObject>();
        private void Awake()
        {
            for (int i = 0; i < healthManager.MaxHealth; i++)
            {
                GameObject icon = Instantiate(healthIconPrefab, this.transform);
                _instantiatedIcons.Add(icon);
            }
        }
        
        private void OnDamage(int before, int after)
        {
            for (int i = after; i < before; i++)
            {
                _instantiatedIcons[i].SetActive(false);
            }
        }
        
        private void OnEnable()
        {
            healthManager.onDamage += OnDamage;
        }
        private void OnDisable()
        {
            healthManager.onDamage -= OnDamage;
        }
    }
}