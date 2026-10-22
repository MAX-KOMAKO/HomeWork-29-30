using UnityEngine;

[System.Serializable]
public class DragonConfig
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _fireDamage;
    [SerializeField] private int _armor;

    public GameObject Prefab => _prefab;
    public int FireDamage => _fireDamage;
    public int Armor => _armor;
}