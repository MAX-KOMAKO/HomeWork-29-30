using UnityEngine;

namespace Enemies
{
    public class Ork : Enemy
    {
        private int _damage;
        private int _health;

        public void Setup(int damage, int health)
        {
            _damage = damage;
            _health = health;
        }

        public override void PrintStats()
        {
            Debug.Log($"Орк создан. Урон: {_damage}, Здоровье: {_health}");
        }
    }
}