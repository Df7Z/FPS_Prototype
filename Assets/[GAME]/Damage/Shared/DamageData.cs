using System;
using UnityEngine;

namespace Game.Damage
{
    [Serializable]
    public class DamageData
    {
        [SerializeField] [Min(1)] private uint _count = 10;

        public uint Count => _count;
    }
}