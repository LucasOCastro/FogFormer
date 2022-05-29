using System;
using System.Collections.Generic;
using UnityEngine;

namespace FogFormer
{
    public class KillZoneTrigger : MonoBehaviour
    {
        [SerializeField] private Triggerable[] triggerables;
        [SerializeField] private List<HealthManager> targets;
        [SerializeField] private LayerMask mask;
        
        
        private int _aliveCount;
        private void OnEnable()
        {
            Collider2D colliders = GetComponent<Collider2D>();
            GetTargetsInCollider(colliders);
            _aliveCount = targets.Count;

            foreach (HealthManager target in targets)
            {
                target.OnDeath += OnTargetDeath;
            }
        }

        private void OnDisable()
        {
            foreach (HealthManager target in targets)
            {
                target.OnDeath -= OnTargetDeath;
            }
            targets.Clear();
        }

        private void OnTargetDeath()
        {
            _aliveCount--;
            if (_aliveCount == 0)
            {
                Trigger();
                this.enabled = false;
            }
        }

        private void Trigger()
        {
            foreach (var triggerable in triggerables)
            {
                triggerable.Trigger();
            }
        }
        
        private void GetTargetsInCollider(Collider2D col)
        {
            List<Collider2D> overlap = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D() {layerMask = mask, useLayerMask = true};
            int inArea = col.OverlapCollider(filter, overlap);
            for (int i = 0; i < inArea; i++)
            {
                HealthManager health = overlap[i].GetComponent<HealthManager>();
                if (health == null) continue;
                if (health != null && !targets.Contains(health))
                {
                    targets.Add(health);    
                }
            }
        }
    }
}