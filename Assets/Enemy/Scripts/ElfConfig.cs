using UnityEngine;

namespace Enemies
{
    [System.Serializable]
    public class ElfConfig
    {
        [SerializeField] private Elf _prefab;
        [SerializeField] private int _mana;
        [SerializeField] private float _agility;

        public Elf Prefab => _prefab;
        public int Mana => _mana;
        public float Agility => _agility;
    }
}