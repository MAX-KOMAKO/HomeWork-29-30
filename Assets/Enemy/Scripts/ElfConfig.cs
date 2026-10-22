using UnityEngine;

[System.Serializable]
public class ElfConfig
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _mana;
    [SerializeField] private float _agility;

    public GameObject Prefab => _prefab;
    public int Mana => _mana;
    public float Agility => _agility;
}