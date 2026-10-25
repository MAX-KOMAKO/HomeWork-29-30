using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Ork _orkPrefab;
        [SerializeField] private Elf _elfPrefab;
        [SerializeField] private Dragon _dragonPrefab;

        public Enemy Spawn(BaseConfig config, Vector3 position)
        {
            switch (config)
            {
                case OrkConfig orkConfig:
                    Ork ork = Object.Instantiate(_orkPrefab, position, Quaternion.identity);
                    ork.Setup(orkConfig.Damage, orkConfig.Health);
                    return ork;
                
                case ElfConfig elfConfig:
                    Elf elf = Object.Instantiate(_elfPrefab, position, Quaternion.identity);
                    elf.Setup(elfConfig.Mana, elfConfig.Agility);
                    return elf;
                
                case DragonConfig dragonConfig:
                    Dragon dragon = Object.Instantiate(_dragonPrefab, position, Quaternion.identity);
                    dragon.Setup(dragonConfig.FireDamage, dragonConfig.Armor);
                    return dragon;
                
                default:
                    throw new ArgumentException($"Неизвестный тип конфига: {config.GetType().Name}", nameof(config));
            }
        }
    }
}