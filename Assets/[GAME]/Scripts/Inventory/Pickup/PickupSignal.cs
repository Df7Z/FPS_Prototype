using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class PickupSignal : EcsComponent
    {
        public ItemCollector ItemCollector;

        protected override void OnDespawnPool()
        {
            Delete(this);
        }
    }
}