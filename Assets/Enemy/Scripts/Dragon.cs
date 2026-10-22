using UnityEngine;

namespace Enemies
{
    public class Dragon : Enemy
    {
        private int _fireDamage;
        private int _armor;

        public void Setup(int fireDamage, int armor)
        {
            _fireDamage = fireDamage;
            _armor = armor;
        }

        public override void PrintStats()
        {
            Debug.Log($"Дракон создан. Огненный урон: {_fireDamage}, Броня: {_armor}");
        }
    }
}