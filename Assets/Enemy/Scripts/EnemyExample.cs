using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyExample : MonoBehaviour
{
    private const int EnemiesPerType = 3;
    private const float SpawnAreaRadius = 5f;

    [SerializeField] private EnemySpawner _spawner;
    
    [Header("Настройки Орков")]
    [SerializeField] private OrkConfig[] _orkConfigs;
    
    [Header("Настройки Эльфов")]
    [SerializeField] private ElfConfig[] _elfConfigs;
    
    [Header("Настройки Драконов")]
    [SerializeField] private DragonConfig[] _dragonConfigs;

    private void Start()
    {
        if (_spawner == null)
        {
            Debug.LogError("EnemyExample: не назначен спавнер.");
            return;
        }

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

        for (int i = 0; i < EnemiesPerType; i++)
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
        float positionX = Random.Range(-SpawnAreaRadius, SpawnAreaRadius);
        float positionY = Random.Range(-SpawnAreaRadius, SpawnAreaRadius);
        return new Vector3(positionX, positionY, 0f);
    }
}