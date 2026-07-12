using UnityEngine;
using Game.Combat;

namespace Game.AI
{
    public class EnemyMeleeAttack : MonoBehaviour, IAttack
    {
        [SerializeField] private int damage=10;
        [SerializeField] private float hitRange=2f;


        public void Attack(Transform player)
        {
            if(Vector3.Distance(transform.position,player.position)<=hitRange)
            {
                player.GetComponent<IDamageable>().TakeDamage(damage);
                EnemyEvents.EnemyAttacked();
            }
            
        }
    }
}