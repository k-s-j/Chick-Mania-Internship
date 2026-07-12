using UnityEngine;

namespace FPS.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Enemy")]
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform enemySpawnPoint;

        [Header("Weapons")]
        [SerializeField] private GameObject pistolPrefab;
        [SerializeField] private GameObject grenadeLauncherPrefab;
        [SerializeField] private Transform weaponHolder;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            SpawnEnemy();
            SpawnWeapons();
        }

        private void SpawnEnemy()
        {
            Instantiate(enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
        }

        private void SpawnWeapons()
        {
            GameObject pistol = Instantiate(pistolPrefab, weaponHolder);
            GameObject grenadeLauncher = Instantiate(grenadeLauncherPrefab, weaponHolder);

            pistol.SetActive(true);
            grenadeLauncher.SetActive(false);
        }
    }
}