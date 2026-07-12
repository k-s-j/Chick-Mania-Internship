using UnityEngine;

namespace FPS.Damage
{
    public interface IDamageable
    {
        void TakeDamage(float damage);
        void Die();
    }
}