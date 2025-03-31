using System;
using UnityEngine;

namespace Game.Player.Move
{
    [Serializable]
    internal class CmdMove
    {
        [Min(0f)] [SerializeField] private float _speed;
        
        [Min(0f)] [SerializeField] private float _acceleration;

        [Min(0f)] [SerializeField] private float _deacceleration;
        public float Speed => _speed;
        public float Acceleration => _acceleration;
        public float Deacceleration => _deacceleration;
    }
}