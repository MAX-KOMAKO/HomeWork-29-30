using UnityEngine;
using Object = UnityEngine.Object;

namespace Enemies
{
    public class EnemySpawner
    {
        private const string INVALID_ORK_CONFIG_ERROR = "Некорректная конфигурация для спавна орка.";
        private const string INVALID_ELF_CONFIG_ERROR = "Некорректная конфигурация для спавна эльфа.";
        private const string INVALID_DRAGON_CONFIG_ERROR = "Некорректная конфигурация для спавна дракона.";

        public Ork SpawnOrk(OrkConfig config, Vector3 position)
        {
            if (config == null || config.Prefab == null)
            {
                Debug.LogError(INVALID_ORK_CONFIG_ERROR);
                return null;
            }

            Ork ork = Object.Instantiate(config.Prefab, position, Quaternion.identity);
            ork.Setup(config.Damage, config.Health);
            return ork;
        }

        public Elf SpawnElf(ElfConfig config, Vector3 position)
        {
            if (config == null || config.Prefab == null)
            {
                Debug.LogError(INVALID_ELF_CONFIG_ERROR);
                return null;
            }

            Elf elf = Object.Instantiate(config.Prefab, position, Quaternion.identity);
            elf.Setup(config.Mana, config.Agility);
            return elf;
        }

        public Dragon SpawnDragon(DragonConfig config, Vector3 position)
        {
            if (config == null || config.Prefab == null)
            {
                Debug.LogError(INVALID_DRAGON_CONFIG_ERROR);
                return null;
            }

            Dragon dragon = Object.Instantiate(config.Prefab, position, Quaternion.identity);
            dragon.Setup(config.FireDamage, config.Armor);
            return dragon;
        }
    }
}