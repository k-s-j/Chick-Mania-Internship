using UnityEngine;
using Game.Combat;

namespace Game.AI
{
    public class EnemyProjectileAttack : MonoBehaviour, ISpecialAttack
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float projectileForce=15f;

        public void SpecialAttack()
        {
                Debug.Log("SPECIAL ATTACK");
            GameObject projectile =Instantiate(projectilePrefab,shootPoint.position,shootPoint.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(shootPoint.forward*projectileForce,ForceMode.Impulse);

            EnemyEvents.EnemyUsedSpecialAttack();
        }
    }
}