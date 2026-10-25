using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemySpawnExample : MonoBehaviour
    {
        private const int ENEMIES_PER_TYPE = 3;
        private const float SPAWN_AREA_RADIUS = 5f;

        [Header("Настройки Орков")]
        [SerializeField] private OrkConfig[] _orkConfigs;

        [Header("Настройки Эльфов")]
        [SerializeField] private ElfConfig[] _elfConfigs;

        [Header("Настройки Драконов")]
        [SerializeField] private DragonConfig[] _dragonConfigs;

        [SerializeField] private EnemySpawner _spawner;

        private void Start()
        {
            SpawnRandomEnemies(_orkConfigs);
            SpawnRandomEnemies(_elfConfigs);
            SpawnRandomEnemies(_dragonConfigs);
        }

        private void SpawnRandomEnemies(BaseConfig[] configs)
        {
            if (configs == null || configs.Length == 0)
            {
                Debug.LogWarning("Массив конфигураций пуст, спавн пропущен.");
                return;
            }

            for (int i = 0; i < ENEMIES_PER_TYPE; i++)
            {
                BaseConfig randomConfig = configs[Random.Range(0, configs.Length)];
                Vector3 randomPosition = GetRandomPosition();

                Enemy enemy = _spawner.Spawn(randomConfig, randomPosition);
                
                if (enemy != null)
                    enemy.PrintStats();
            }
        }

        private Vector3 GetRandomPosition()
        {
            float positionX = Random.Range(-SPAWN_AREA_RADIUS, SPAWN_AREA_RADIUS);
            float positionY = Random.Range(-SPAWN_AREA_RADIUS, SPAWN_AREA_RADIUS);
            return new Vector3(positionX, positionY, 0f);
        }
    }
}