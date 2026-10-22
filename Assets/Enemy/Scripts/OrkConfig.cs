using UnityEngine;

[System.Serializable]
public class OrkConfig
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;

    public GameObject Prefab => _prefab;
    public int Damage => _damage;
    public int Health => _health;
}