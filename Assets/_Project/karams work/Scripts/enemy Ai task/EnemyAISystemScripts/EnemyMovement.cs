using UnityEngine;
using UnityEngine.AI;

namespace Game.AI
{
    public class EnemyMovement : MonoBehaviour
    {
        private NavMeshAgent agent;
        [SerializeField] private float repositionDistance = 4f;

        private void Awake()
        {
            agent=GetComponent<NavMeshAgent>();
        }

        public void Chase(Transform player)
        {
            agent.isStopped=false;
            agent.SetDestination(player.position);
        }

        public void Stop()
        {
            agent.isStopped=true;
        }

        public void RepositionAroundPlayer(Transform player)
        {
            Vector3 random = Random.insideUnitSphere * repositionDistance;
            random.y = 0;

            if(NavMesh.SamplePosition(player.position+random,out NavMeshHit hit,repositionDistance,NavMesh.AllAreas))
            {
                agent.isStopped=false;
                agent.SetDestination(hit.position);
            }
        }
    }
}