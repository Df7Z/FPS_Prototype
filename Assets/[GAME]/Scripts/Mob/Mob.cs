using System;
using ECS_MONO;
using Game.AI.Shared;
using UnityEngine;

namespace Game.Mobs
{
    internal sealed class Mob : EcsComponentMono
    {
        [SerializeField] private AISource _ai;

        public AISource AI => _ai;
        
        protected override void OnSpawnPool()
        {
            Owner.SafeAdd<StartAISignal>(); //Start AI logic
        }
    }
}