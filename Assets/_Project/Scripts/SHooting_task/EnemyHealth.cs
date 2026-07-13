using UnityEngine;

namespace FPS.Damage
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth = 100f;

        private float currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            Debug.Log("Enemy took damage: " + damage);
            Debug.Log("Enemy health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Debug.Log("Enemy destroyed");
            Destroy(gameObject);
        }
    }
}