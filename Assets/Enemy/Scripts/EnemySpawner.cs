using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const string InvalidOrkConfigError = "Некорректная конфигурация для спавна орка.";
    private const string InvalidElfConfigError = "Некорректная конфигурация для спавна эльфа.";
    private const string InvalidDragonConfigError = "Некорректная конфигурация для спавна дракона.";
    private const string MissingOrkComponentError = "В префабе орка отсутствует компонент Ork.";
    private const string MissingElfComponentError = "В префабе эльфа отсутствует компонент Elf.";
    private const string MissingDragonComponentError = "В префабе дракона отсутствует компонент Dragon.";

    public Ork SpawnOrk(OrkConfig config, Vector3 position)
    {
        if (config == null || config.Prefab == null)
        {
            Debug.LogError(InvalidOrkConfigError);
            return null;
        }

        GameObject instance = Instantiate(config.Prefab, position, Quaternion.identity);
        
        if (instance.TryGetComponent(out Ork ork) == false)
        {
            Debug.LogError(MissingOrkComponentError);
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
            Debug.LogError(InvalidElfConfigError);
            return null;
        }

        GameObject instance = Instantiate(config.Prefab, position, Quaternion.identity);
        
        if (instance.TryGetComponent(out Elf elf) == false)
        {
            Debug.LogError(MissingElfComponentError);
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
            Debug.LogError(InvalidDragonConfigError);
            return null;
        }

        GameObject instance = Instantiate(config.Prefab, position, Quaternion.identity);
        
        if (instance.TryGetComponent(out Dragon dragon) == false)
        {
            Debug.LogError(MissingDragonComponentError);
            Destroy(instance);
            return null;
        }

        dragon.Setup(config.FireDamage, config.Armor);
        return dragon;
    }
}