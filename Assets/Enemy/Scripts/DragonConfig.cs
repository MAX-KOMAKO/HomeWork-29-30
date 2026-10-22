using UnityEngine;

namespace Enemies
{
    [System.Serializable]
    public class DragonConfig
    {
        [SerializeField] private Dragon _prefab;
        [SerializeField] private int _fireDamage;
        [SerializeField] private int _armor;

        public Dragon Prefab => _prefab;
        public int FireDamage => _fireDamage;
        public int Armor => _armor;
    }
}