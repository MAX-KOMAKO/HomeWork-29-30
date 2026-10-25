using System;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class ElfConfig : BaseConfig
    {
        [SerializeField] private int _mana;
        [SerializeField] private float _agility;

        public int Mana => _mana;
        public float Agility => _agility;
    }
}