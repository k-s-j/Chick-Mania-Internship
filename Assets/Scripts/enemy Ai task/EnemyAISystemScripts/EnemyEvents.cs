using System;

namespace Game.AI
{
    public static class EnemyEvents
    {
        public static event Action OnEnemyAttack;
        public static event Action OnEnemySpecialAttack;

        public static void EnemyAttacked()
        {
            OnEnemyAttack?.Invoke();
        }
        public static void EnemyUsedSpecialAttack()
        {
            OnEnemySpecialAttack?.Invoke();
        }
    }
}