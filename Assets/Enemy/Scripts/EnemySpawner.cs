using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Ork SpawnOrk(OrkConfig config, Vector3 position)
    {
        if (config == null || config.Prefab == null)
        {
            Debug.LogError("Некорректная конфигурация для спавна орка.");
            return null;
        }

        GameObject instance = Instantiate(config.Prefab, position, Quaternion.identity);
        
        if (instance.TryGetComponent(out Ork ork) == false)
        {
            Debug.LogError("В префабе орка отсутствует компонент Ork.");
            Destroy(instance);
            return null;
        }

        ork.Setup(config.Damage, config.Health);
        return ork;
    }

    public Elf SpawnElf(ElfConfig config, Vector3 position)
    {
        if (config == null || config.Prefab == null)
        {
            Debug.LogError("Некорректная конфигурация для спавна эльфа.");
            return null;
        }

        GameObject instance = Instantiate(config.Prefab, position, Quaternion.identity);
        
        if (instance.TryGetComponent(out Elf elf) == false)
        {
            Debug.LogError("В префабе эльфа отсутствует компонент Elf.");
            Destroy(instance);
            return null;
        }

        elf.Setup(config.Mana, config.Agility);
        return elf;
    }

    public Dragon SpawnDragon(DragonConfig config, Vector3 position)
    {
        if (config == null || config.Prefab == null)
        {
            Debug.LogError("Некорректная конфигурация для спавна дракона.");
            return null;
        }

        GameObject instance = Instantiate(config.Prefab, position, Quaternion.identity);
        
        if (instance.TryGetComponent(out Dragon dragon) == false)
        {
            Debug.LogError("В префабе дракона отсутствует компонент Dragon.");
            Destroy(instance);
            return null;
        }

        dragon.Setup(config.FireDamage, config.Armor);
        return dragon;
    }
}