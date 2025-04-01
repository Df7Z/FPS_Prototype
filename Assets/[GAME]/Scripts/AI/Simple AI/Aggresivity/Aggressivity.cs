using System;
using ECS_MONO;
using UnityEngine;

namespace Game.AI
{
    internal sealed class Aggressivity : EcsComponentMono
    {
        [SerializeField] private float _radius = 5f;

        [NonSerialized] public bool Once;
        
        public float Radius => _radius;

        protected override void OnDespawnPool()
        {
            Once = false;
        }
    }
}