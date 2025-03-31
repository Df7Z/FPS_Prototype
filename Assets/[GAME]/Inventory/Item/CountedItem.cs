using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    [RequireComponent(typeof(CountedItemRuntime))]
    internal sealed class CountedItem : EcsComponentMono //Default values
    {
        public override uint Order => 1000;
        
        [SerializeField] [Min(1)] private int _max = 64;
        [SerializeField] [Min(1)] private int _current = 1;

        public int Max => _max;
        public int Current => _current;
        
        protected override void OnRegisterEntity(IEntity entity)
        {
            entity.Get<CountedItemRuntime>().Current = Current;
        }
        
        public override void OnSpawnPool(IEntity entity)
        {
            entity.Get<CountedItemRuntime>().Current = Current;
        }
    }
}