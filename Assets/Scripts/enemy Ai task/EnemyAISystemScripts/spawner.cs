using System;
using Game.AI;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab; 

    [SerializeField] Vector3 playerSpawnPosition = new Vector3 (0,0,-25);
    [SerializeField] Vector3 enemySpawnPosition = new Vector3 (0,0,0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = Instantiate(playerPrefab,playerSpawnPosition, Quaternion.identity);
        GameObject enemy = Instantiate(enemyPrefab,enemySpawnPosition, Quaternion.identity);
        
        enemy.GetComponent<EnemyAI>().SetPlayer(player.transform);
    }


}
