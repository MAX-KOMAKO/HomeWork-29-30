using System;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class DragonConfig : BaseConfig
    {
        [SerializeField] private int _fireDamage;
        [SerializeField] private int _armor;

        public int FireDamage => _fireDamage;
        public int Armor => _armor;
    }
}