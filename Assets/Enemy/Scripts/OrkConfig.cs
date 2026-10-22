using UnityEngine;

namespace Enemies
{
    [System.Serializable]
    public class OrkConfig
    {
        [SerializeField] private Ork _prefab;
        [SerializeField] private int _damage;
        [SerializeField] private int _health;

        public Ork Prefab => _prefab;
        public int Damage => _damage;
        public int Health => _health;
    }
}