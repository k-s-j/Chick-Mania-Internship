using UnityEngine;
using Game.Combat;

namespace Game.AI
{
    public class LargeProjectile : MonoBehaviour
    {
        [SerializeField] private int damage=25;

        private void Start()
        {
            Destroy(gameObject,5f);
        }

        private void OnCollisionEnter(Collision collider)
        {
            if (collider.gameObject.GetComponent<IDamageable>() != null)
            collider.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}