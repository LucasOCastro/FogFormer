using System;
using UnityEngine;

namespace FogFormer
{
    public class TriggerArea : MonoBehaviour
    {
        [SerializeField] private bool singleTime;
        [SerializeField] private Triggerable[] triggerables;

        private void OnTriggerEnter2D(Collider2D col)
        {
            foreach (var triggerable in triggerables)
            {
                triggerable.Trigger();
            }
            
            if (singleTime)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}