using UnityEngine;
using Game.Combat;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Awake() => currentHealth = maxHealth;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);
        if (currentHealth <= 0) 
        {
            Debug.Log("Player Died");
        }
    }
}