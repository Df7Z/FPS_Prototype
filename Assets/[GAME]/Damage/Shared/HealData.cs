using System;
using UnityEngine;

namespace Game.Damage
{
    [Serializable]
    public class HealData
    {
        [SerializeField] [Min(1)] private uint _count = 10;
        [SerializeField] private bool _bonus = false;

        public bool Bonus => _bonus;

        public uint Count => _count;
    }
}