using UnityEngine;

namespace FPS.Damage
{
    public class Hitbox : MonoBehaviour
    {
        public enum HitboxType
        {
            Body,
            Head
        }

        [SerializeField] private HitboxType hitboxType;
        [SerializeField] private EnemyHealth enemyHealth;

        public HitboxType Type => hitboxType;
        public EnemyHealth Health => enemyHealth;
    }
}