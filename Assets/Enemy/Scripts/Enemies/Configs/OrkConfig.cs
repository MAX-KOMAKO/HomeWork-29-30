using System;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class OrkConfig : BaseConfig
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _health;

        public int Damage => _damage;
        public int Health => _health;
    }
}