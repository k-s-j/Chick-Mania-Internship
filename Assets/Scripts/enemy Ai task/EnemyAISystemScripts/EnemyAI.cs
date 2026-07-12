using UnityEngine;
using Game.Combat;

namespace Game.AI
{

    public class EnemyAI : MonoBehaviour
    {
        private enum EnemyState
        {
            Chase,
            Attack,
            Reposition
        }
        [SerializeField] private Transform player;
        [SerializeField] private EnemyMovement movement;
        [SerializeField] private MonoBehaviour normalAttackScript;
        [SerializeField] private MonoBehaviour specialAttackScript;
        [SerializeField] private float attackRange=20f;
        [SerializeField] private float attackCooldown=2f;
        [SerializeField] private float specialAttackChance=0.15f;
        [SerializeField] private Animator animator;
        bool canatack;
        private EnemyState currentState = EnemyState.Chase;
        private IAttack attack;
        private ISpecialAttack special;
        private float nextAttack;
        private float nextchase;

        private void Awake()
        {
            attack = normalAttackScript as IAttack;
            special = specialAttackScript as ISpecialAttack;
  
        }

        private void Update()
        {
            switch (currentState)
            {   case EnemyState.Chase:
                    ChaseState();
                    break;

                case EnemyState.Attack:
                    AttackState();
                    break;

                case EnemyState.Reposition:
                    RepositionState();
                    break;
            }
        }
            
        private void ChaseState()
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= attackRange)
            {
                ChangeState(EnemyState.Attack);
                return;
            }
            if (Time.time >= nextchase)
            {
                movement.Chase(player);
                animator.SetBool("Attack?", false);
                nextchase = Time.time + 1f;
            }
            if (Time.time >= nextAttack)
            {
                attack?.Attack(player);

                nextAttack = Time.time + attackCooldown;

                if (Random.value <= specialAttackChance)
                {
                    special?.SpecialAttack();
                }

            }
        }
        private void RepositionState()
        {
            movement.RepositionAroundPlayer(player);
            ChangeState(EnemyState.Chase);
        }

        private void AttackState()
        {
            movement.Stop();
            animator.SetBool("Attack?", true);
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > attackRange)
            {
                ChangeState(EnemyState.Chase);
                return;
            }
            if (Time.time >= nextAttack)
            {
                
                attack?.Attack(player);

                nextAttack = Time.time + attackCooldown;
                {
                    ChangeState(EnemyState.Reposition);
                }
            }

        }
        private void ChangeState(EnemyState newState)
        {
            currentState = newState;
        }
        public void SetPlayer(Transform gottenplayer)
        {
            player = gottenplayer;
        }
    }
}
