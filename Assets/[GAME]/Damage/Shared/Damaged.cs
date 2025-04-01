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
        
        public override void OnDespawnPool(IEntity entity) => ResetToDefault();

        protected override void OnRegisterEntity(IEntity entity) => ResetToDefault();
        
        public void ResetToDefault()
        {
            if (Owner.Has<DamagedDead>())
            {
                Owner.Del<DamagedDead>();
            }
            
            if (!Owner.Has<DamagedRuntime>())
            {
                Owner.Add<DamagedRuntime>();
            }
            
            Owner.Get<DamagedRuntime>().Set(enduranceDefault);
        }
    }
}