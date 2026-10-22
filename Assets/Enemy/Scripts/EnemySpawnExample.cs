using System;
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

        private EnemySpawner _spawner;

        private void Awake()
        {
            _spawner = new EnemySpawner();
        }

        private void Start()
        {
            SpawnRandomEnemies(_orkConfigs, _spawner.SpawnOrk);
            SpawnRandomEnemies(_elfConfigs, _spawner.SpawnElf);
            SpawnRandomEnemies(_dragonConfigs, _spawner.SpawnDragon);
        }

        private void SpawnRandomEnemies<TConfig>(TConfig[] configs, Func<TConfig, Vector3, Enemy> spawnMethod)
        {
            if (configs == null || configs.Length == 0)
            {
                Debug.LogWarning("Массив конфигураций пуст, спавн пропущен.");
                return;
            }

            for (int i = 0; i < ENEMIES_PER_TYPE; i++)
            {
                TConfig randomConfig = configs[Random.Range(0, configs.Length)];
                Vector3 randomPosition = GetRandomPosition();

                Enemy enemy = spawnMethod(randomConfig, randomPosition);
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