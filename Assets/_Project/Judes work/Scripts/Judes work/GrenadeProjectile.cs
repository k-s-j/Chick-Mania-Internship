using UnityEngine;
using FPS.Damage;

namespace FPS.Weapons
{
    public class GrenadeProjectile : MonoBehaviour
    {
        [SerializeField] private float explosionRadius = 5f;
        [SerializeField] private float explosionDamage = 40f;
        [SerializeField] private GameObject explosionEffect;

        private bool exploded;

        private void OnCollisionEnter(Collision collision)
        {
            Explode();
        }

        private void OnTriggerEnter(Collider other)
        {
            Explode();
        }

        private void Explode()
        {
            if (exploded) return;
            exploded = true;

            Debug.Log("GRENADE EXPLODED");

            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider hit in hits)
            {
                Hitbox hitbox = hit.GetComponent<Hitbox>();

                if (hitbox != null)
                {
                    hitbox.Health.TakeDamage(explosionDamage);
                }
            }

            Destroy(gameObject);
        }
    }
}