using System;
using ECS_MONO;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Damage
{
    public sealed class Damaged : EcsComponentMono
    {
        public override uint Order => 1000;

        [FormerlySerializedAs("_endurance")] [SerializeField] [Min(1)] private uint enduranceDefault = 100;

        public uint EnduranceDefault => enduranceDefault;
        
        public override void OnDespawnPool(IEntity entity) => Initialize(entity);

        protected override void OnRegisterEntity(IEntity entity) => Initialize(entity);
        
        private void Initialize(IEntity entity)
        {
            if (!entity.Has<DamagedRuntime>())
            {
                entity.Add<DamagedRuntime>();
            }
            
            entity.Get<DamagedRuntime>().Set(enduranceDefault);
        }
    }
}