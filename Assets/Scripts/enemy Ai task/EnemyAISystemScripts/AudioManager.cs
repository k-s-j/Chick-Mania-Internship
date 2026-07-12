using UnityEngine;
using Game.AI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if(Instance!=null && Instance!=this){Destroy(gameObject);return;}
        Instance=this;
    }

    private void OnEnable()
    {
        EnemyEvents.OnEnemyAttack+=PlayEnemyAttackSound;
        EnemyEvents.OnEnemySpecialAttack+=PlayEnemySpecialSound;
    }

    private void OnDisable()
    {
        EnemyEvents.OnEnemyAttack-=PlayEnemyAttackSound;
        EnemyEvents.OnEnemySpecialAttack-=PlayEnemySpecialSound;
    }

    void PlayEnemyAttackSound(){Debug.Log("Enemy Attack Sound");}
    void PlayEnemySpecialSound(){Debug.Log("Enemy Special Sound");}
}