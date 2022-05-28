using UnityEngine;

namespace FogFormer
{
    public class HealthPickup : MonoBehaviour
    {
        [SerializeField] private int healAmount;
        private void OnTriggerEnter2D(Collider2D col)
        {
            HealthManager health = col.GetComponent<HealthManager>();
            if (health == null || health.Health == health.MaxHealth)
            {
                return;
            }
            health.Heal(healAmount);
            Destroy(this.gameObject);
        }
    }
}
