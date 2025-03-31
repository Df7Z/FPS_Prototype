using ECS_MONO;
using UnityEngine;

namespace Game.Inventory
{
    internal sealed class ItemCollectorSystem : EcsSystemMono<ItemCollector>
    {
        private Collider[] _colliders = new Collider[4];
        
        protected override void FixedRun(EntityMono e, ItemCollector c1)
        {
            TryCollect(c1);
        }

        private void TryCollect(ItemCollector collector)
        {
            var size = Physics.OverlapSphereNonAlloc(collector.CollectCenter.position, collector.Radius, _colliders, ~LayerMask.NameToLayer("Item"));
           
            if (size == 0) return;

            for (int i = 0; i < size; i++)
            {
                if (_colliders[i].TryGetComponent(out EntityReference entityReference))
                {
                    if (entityReference.Entity.Has<Item>())
                    {
                        if (!entityReference.Entity.Has<PickupSignal>())
                        {
                            var signal = entityReference.Entity.Add<PickupSignal>();

                            signal.ItemCollector = collector;
                            signal.PickupItem = entityReference.Entity.Get<Item>();
                        }
                    }
                }
            }
        }
    }
}